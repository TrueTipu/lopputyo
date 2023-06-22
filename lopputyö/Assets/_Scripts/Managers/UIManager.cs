using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] ItemUI itemChooseUI;

    public void ActivateItemChoose(bool _value, List<AbilityManager.AbilityPacket> _abilityPackets, StreamCore _core)
    {
        itemChooseUI.gameObject.SetActive(_value);
        itemChooseUI.Load(_abilityPackets, _core);

    }

    public void ChangeScene(int _index)  { SceneLoader.Instance.ChangeScene(_index); }
    public void NextScene() {  SceneLoader.Instance.NextScene(); }
    public void ReloadScene() {  SceneLoader.Instance.ReloadScene(); }
}
