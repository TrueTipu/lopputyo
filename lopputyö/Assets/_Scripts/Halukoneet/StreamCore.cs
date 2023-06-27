using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class StreamCore : MonoBehaviour
{
    bool powered;
    public AbilityManager.AbilityPacket CurrentAbility { get;  private set; }

    bool onTrigger;

    UIManager uIManager; //safety reasons to avoid singleton problems

    AbilityManager playerAbilities;

    


    private void Start()
    {
        uIManager = UIManager.Instance;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onTrigger = true;
            playerAbilities = collision.GetComponent<AbilityManager>();
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
                uIManager.ActivateItemChoose(true, playerAbilities.Abilities, this);
            }
            else
            {
                SceneLoader.LoadLevelEditor();
            }
        }
    }
    public void SetAbility(AbilityManager.AbilityPacket _ability, Action<List<AbilityManager.AbilityPacket>> _reloadCallback)
    {
        if(CurrentAbility != null)
        {
            playerAbilities.ActivateAbility(CurrentAbility.AbilityTag);
        }

        CurrentAbility = _ability;

        if(_ability != null)
        {
            powered = true;
            playerAbilities.DeActivateAbility(_ability.AbilityTag);
        }
        
        _reloadCallback(playerAbilities.Abilities);
    }


}
