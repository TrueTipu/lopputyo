using UnityEngine;
using System.Collections;

public class StreamCore : MonoBehaviour
{
    bool powered;

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

            }
        }
    }
    public void SetAbility(PlayerAbility _abilityTag)
    {
        return;
    }
}
