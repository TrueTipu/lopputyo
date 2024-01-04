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
    [GetSO] RoomSet roomSet; //tämäkin vain sen yhden intin takia
    [GetSO] ActiveStreamsData streamData; //tämäkin vain yhden variablen takia

    List<PlayerAbility> abilities;

    [SerializeField] GameObject chosenAbilitySquare;

    [SerializeField] GameObject openEditorButton;
    [SerializeField] GameObject openEditorButtonFake;


    [GetSO] AbilityData abilityData;

    Action<PlayerAbility> abilityCallback;



    public void Start()
    {
        this.InjectGetSO();

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

    public void Load(HashSet<PlayerAbility> _abilities, CoreData _core)
    {

        //Debug.Log("2 [" + string.Join(", ", _abilities.ToList().ConvertAll<string>((x) => x.ToString()).ToArray()) + "]");
        for (int i = 0; i < abilitySquares.Length; i++)
        {
            if (i < _abilities.Count)
            {
                abilitySquares[i].SetActive(true);
                Text _text = abilitySquares[i].GetComponentInChildren<Text>();
                _text.text = _abilities.ToList()[i].ToString();
            }

            else { abilitySquares[i].SetActive(false); }
        }

        lastCore = _core;
        abilities = _abilities.ToList();

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
        //abilitySquares.ToList().ForEach((x) => { x.SetActive(false); });

        if(_index != -1)
        {
            AudioManager.Instance.Play("Aktivoi");
            lastCore.SetAbility(abilities[_index]);
            openEditorButton.SetActive(true);
            openEditorButtonFake.SetActive(false);

        }
        else
        {


            if (roomSet.CurrentStreamLevel >= streamData.ActiveStreamAmount)
            {
                return;
            }
            AudioManager.Instance.Play("Sammuta");

            lastCore.SetAbility(PlayerAbility.None);
            openEditorButton.SetActive(false);
            openEditorButtonFake.SetActive(true);
        }
    }
        

    public void CloseUI()
    {
        AudioManager.Instance.Play("Valikko");
        abilitySquares.ToList().ForEach((x) => { x.SetActive(false); });
        uIData.SetItemUI(false);
        gameObject.SetActive(false);
    }

}
