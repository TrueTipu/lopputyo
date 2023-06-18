using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PickUpItem : MonoBehaviour
{
    [SerializeField] PlayerAbility ability;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AbilityManager _abilityManager = collision.GetComponent<AbilityManager>();

            //cool effekts här
            _abilityManager.ActivateAbility(ability);

            Destroy(gameObject);
        }
    }
}
