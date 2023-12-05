using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [SerializeField] ItemUI itemChooseUI;
    [GetSO] UIData uIData;

    [SerializeField] GameObject roomCountUI;
    [SerializeField] TMPro.TextMeshProUGUI countText;
    [GetSO] RoomSet roomSet; //VAIHDA MYÖHEMMIN, vain koska next room

    private void OnEnable()
    {
        this.InjectGetSO();
    }
    private void Start()
    {


        uIData.SubscribeItemUIActivated(ActivateItemChoose);
        uIData.SubscribeRoomsLeftChanged(ActivateRoomCount);

        if (uIData.ItemUIActive && itemChooseUI != null)
        {
            uIData.SetupItemUI();
        }

        ActivateRoomCount(uIData.RoomsLeft);

    }

    private void Update()
    {
        if(roomSet.GetNextRooms().Count != uIData.RoomsLeft)
        {
            uIData.SetRoomsLeft(roomSet.GetNextRooms().Count);
        }
    }
    void ActivateRoomCount(int _amount)
    {
        if (roomCountUI == null) return;

        if(_amount != 0)
        {
            roomCountUI.SetActive(true);
            countText.text = _amount.ToString();
        }
        else
        {
            roomCountUI.SetActive(false);
        }
    }
    public void ActivateItemChoose(AbilityData  _abilityData, CoreData _core)
    {
        itemChooseUI.gameObject.SetActive(true);
        itemChooseUI.Load(_abilityData.ActiveAbilities, _core);
        uIData.SetItemUI(true);
    }

    public void ExitEditor()
    {
        if (!uIData.HasRoomsLeft)
        {
            ChangeScene(0);
        }
    }

    public void ChangeScene(int _index)  { SceneLoader.ChangeScene(_index); }
    public void NextScene() {  SceneLoader.NextScene(); }
    public void ReloadScene() {  SceneLoader.ReloadScene(); }
}
