using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PickUpItem : MonoBehaviour
{
    [SerializeField] PlayerAbility ability;

    [SerializeField] AbilityData abilityData;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            //cool effekts här
            abilityData.AddActiveAbilities(ability);

            Destroy(gameObject);
        }
    }
}
