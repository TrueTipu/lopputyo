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
    StreamCore core;
    List<AbilityManager.AbilityPacket> abilityPackets;

    [SerializeField] GameObject chosenAbilitySquare;

    [SerializeField] GameObject openEditorButton;
    [SerializeField] GameObject openEditorButtonFake;


    public void Load(List<AbilityManager.AbilityPacket> _abilityPackets, StreamCore _core)
    {
        _abilityPackets = _abilityPackets.FindAll((x) => { return x.Enabled; });
        for (int i = 0; i < _abilityPackets.Count; i++)
        {
            abilitySquares[i].SetActive(true);
            Text _text = abilitySquares[i].GetComponentInChildren<Text>();
            _text.text = _abilityPackets[i].abilityName;
        }
        core = _core;
        abilityPackets = _abilityPackets;

        if (core.CurrentAbility != null)
        {
            chosenAbilitySquare.SetActive(true);
            chosenAbilitySquare.GetComponentInChildren<Text>().text = core.CurrentAbility.abilityName;
        }
        else
        {
            chosenAbilitySquare.SetActive(false);
        }
    }


    public void SetAbility(int _index)
    {
        abilitySquares.ToList().ForEach((x) => { x.SetActive(false); });

        if(_index != -1)
        {
            core.SetAbility(abilityPackets[_index], (_newAbilityPackets) => { Load(_newAbilityPackets, core); });
            openEditorButton.SetActive(true);
            openEditorButtonFake.SetActive(false);

        }
        else
        {
            core.SetAbility(null, (_newAbilityPackets) => { Load(_newAbilityPackets, core); });
            openEditorButton.SetActive(false);
            openEditorButtonFake.SetActive(true);
        }
    }
        

    void CloseUI()
    {
        abilitySquares.ToList().ForEach((x) => { x.SetActive(false); });
        core = null;
        abilityPackets = null;
        gameObject.SetActive(false);
    }
}
