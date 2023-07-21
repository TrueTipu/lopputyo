using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [SerializeField] ItemUI itemChooseUI;
    [GetSO] UIData uIData;



    private void Start()
    {
        this.InjectGetSO();

        uIData.SubscribeItemUIActivated(ActivateItemChoose);

        if (uIData.ItemUIActive && itemChooseUI != null)
        {
            uIData.SetupItemUI();
        }
    }
    public void ActivateItemChoose(AbilityData  _abilityData, CoreData _core)
    {
        itemChooseUI.gameObject.SetActive(true);
        itemChooseUI.Load(_abilityData.ActiveAbilities, _core);
        uIData.SetItemUI(true);
    }

    public void ChangeScene(int _index)  { SceneLoader.ChangeScene(_index); }
    public void NextScene() {  SceneLoader.NextScene(); }
    public void ReloadScene() {  SceneLoader.ReloadScene(); }
}
