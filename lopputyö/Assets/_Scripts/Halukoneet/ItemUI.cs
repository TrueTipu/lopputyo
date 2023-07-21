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

    [Tooltip("VAIN DEBUGGIA VARTEN")]
    [GetSO] CoreData core;

    [GetSO] UIData uIData;

    List<PlayerAbility> abilities;

    [SerializeField] GameObject chosenAbilitySquare;

    [SerializeField] GameObject openEditorButton;
    [SerializeField] GameObject openEditorButtonFake;


    [SerializeField] AbilityData abilityData;

    Action<PlayerAbility> abilityCallback;
    public void Start()
    {
        this.InjectGetSO();

        abilityCallback = (a) =>
        {
            if (core == null) return;
            Load(abilityData.ActiveAbilities, core);
        };
        abilityData.SubscribeAbilityAdded(abilityCallback);
        abilityData.SubscribeAbilityRemoved(abilityCallback);
    }

    private void Update()
    {
        ///debug
        if (Input.GetKeyDown(KeyCode.T))
        {
            Load(abilityData.ActiveAbilities, core);
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

        core = _core;
        abilities = _abilities;

        if (core.CurrentAbility != PlayerAbility.None)
        {
            chosenAbilitySquare.SetActive(true);
            chosenAbilitySquare.GetComponentInChildren<Text>().text = core.CurrentAbility.ToString();
        }
        else
        {
            chosenAbilitySquare.SetActive(false);
        }

        if(core.Powered)
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
            core.SetAbility(abilities[_index]);
            openEditorButton.SetActive(true);
            openEditorButtonFake.SetActive(false);

        }
        else
        {
            core.SetAbility(PlayerAbility.None);
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
