using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using System.Linq;
using Text = TMPro.TextMeshProUGUI;

public class ItemUI : MonoBehaviour
{
    [SerializeField] GameObject[] abilitySquares;

    CoreData lastCore;

    [GetSO] UIData uIData;

    List<PlayerAbility> abilities;

    [SerializeField] GameObject chosenAbilitySquare;

    [SerializeField] GameObject openEditorButton;
    [SerializeField] GameObject openEditorButtonFake;


    [SerializeField] AbilityData abilityData;

    Action<PlayerAbility> abilityCallback;

    private void OnEnable()
    {
        this.InjectGetSO();
    }

    public void Start()
    {


        abilityCallback = (a) =>
        {
            if (lastCore == null) return;
            Load(abilityData.ActiveAbilities, lastCore);
        };
        abilityData.SubscribeAbilityAdded(abilityCallback);
        abilityData.SubscribeAbilityRemoved(abilityCallback);
    }

    private void Update()
    {
        ///debug
        if (Input.GetKeyDown(KeyCode.T))
        {
            Load(abilityData.ActiveAbilities, lastCore);
        }
    }

    public void Load(List<PlayerAbility> _abilities, CoreData _core)
    {
        //Debug.Log("2 [" + string.Join(", ",_abilities.ConvertAll<string>((x)=>x.ToString()).ToArray()) + "]");
        for (int i = 0; i < abilitySquares.Length; i++)
        {
            if (i < _abilities.Count)
            {
                abilitySquares[i].SetActive(true);
                Text _text = abilitySquares[i].GetComponentInChildren<Text>();
                _text.text = _abilities[i].ToString();
            }

            else { abilitySquares[i].SetActive(false); }
        }

        lastCore = _core;
        abilities = _abilities;

        if (lastCore.CurrentAbility != PlayerAbility.None)
        {
            chosenAbilitySquare.SetActive(true);
            chosenAbilitySquare.GetComponentInChildren<Text>().text = lastCore.CurrentAbility.ToString();
        }
        else
        {
            chosenAbilitySquare.SetActive(false);
        }

        if(lastCore.Powered)
        {
            openEditorButton.SetActive(true);
            openEditorButtonFake.SetActive(false);
        }
        else
        {
            openEditorButton.SetActive(false);
            openEditorButtonFake.SetActive(true);
        }
    }


    public void SetAbility(int _index)
    {
        abilitySquares.ToList().ForEach((x) => { x.SetActive(false); });

        if(_index != -1)
        {
            lastCore.SetAbility(abilities[_index]);
            openEditorButton.SetActive(true);
            openEditorButtonFake.SetActive(false);

        }
        else
        {
            lastCore.SetAbility(PlayerAbility.None);
            openEditorButton.SetActive(false);
            openEditorButtonFake.SetActive(true);
        }
    }
        

    public void CloseUI()
    {
        abilitySquares.ToList().ForEach((x) => { x.SetActive(false); });
        uIData.SetItemUI(false);
        gameObject.SetActive(false);
    }

}
