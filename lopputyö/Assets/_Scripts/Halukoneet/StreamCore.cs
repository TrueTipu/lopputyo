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

    bool onTrigger;

    [SerializeField] AbilityData abilityData;
    [SerializeField] UIData uIData;

    private void Start()
    {
        this.InjectGetSO();

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
            if (coreData.IsMainCore)
            {
                SceneLoader.LoadLevelEditor();
                return;
            }

            uIData.SetupItemUI(abilityData, coreData);

        }
    }





}
