using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class StreamCore : MonoBehaviour
{

    [SerializeField] CoreData coreData;
    public PlayerAbility CurrentAbility
    {
        get => coreData.CurrentAbility;
    }
    bool powered => coreData.Powered;

    bool onTrigger;

    [SerializeField] AbilityData abilityData;
    [SerializeField] UIData uIData;

    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onTrigger = false;
        }
    }

    private void Update()
    {
        if(onTrigger && Keys.InteractKeysDown())
        {
            if (!powered)
            {
                uIData.ActivateItemUI(abilityData, coreData);
            }
            else
            {
                uIData.ItemUIActive = true;
                SceneLoader.LoadLevelEditor();
            }
        }
    }





}
