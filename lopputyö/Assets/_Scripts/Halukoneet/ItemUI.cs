using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Linq;
using Text = TMPro.TextMeshProUGUI;
public class ItemUI : MonoBehaviour
{
    [SerializeField] GameObject[] abilitySquares;
    StreamCore core;
    List<AbilityManager.AbilityPacket> abilityPackets;

    public void Load(List<AbilityManager.AbilityPacket> _abilityPackets, StreamCore _core)
    {
        for (int i = 0; i < _abilityPackets.Count; i++)
        {
            abilitySquares[i].SetActive(true);
            Text _text = abilitySquares[i].GetComponent<Text>();
            _text.text = _abilityPackets[i].abilityName;
        }
        core = _core;
        abilityPackets = _abilityPackets;
    }

    public void SetAbility(int _index)
    {
        core.SetAbility(abilityPackets[_index].AbilityTag);
        abilitySquares.ToList().ForEach((x) => { x.SetActive(false); });
        core = null;
        abilityPackets = null;
        gameObject.SetActive(false);
    }
}
