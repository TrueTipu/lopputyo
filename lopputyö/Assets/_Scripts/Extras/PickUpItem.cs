using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PickUpItem : MonoBehaviour
{
    [SerializeField] PlayerAbility ability;

    [GetSO] AbilityData abilityData;

    private void Start()
    {
        this.InjectGetSO();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            //cool effekts här
            abilityData.AddActiveAbilities(ability);
            AudioManager.Instance.Play("Ability");

            Destroy(gameObject);
        }
    }
}
