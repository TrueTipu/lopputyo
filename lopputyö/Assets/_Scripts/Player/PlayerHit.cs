using UnityEngine;
using System.Collections;

public class PlayerHit : MonoBehaviour
{
    [GetSO] PlayerData playerData;
    private void Start()
    {
        this.InjectGetSO();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //yksinkertaistetusti, toki efektit pitää lisätä action kutsulla esim playerdsatan kautta
            collision.transform.position = playerData.RespawnPoint;
        }
    }
}
