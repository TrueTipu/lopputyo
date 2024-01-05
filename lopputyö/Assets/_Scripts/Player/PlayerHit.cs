using UnityEngine;
using System.Collections;

public class PlayerHit : MonoBehaviour
{
    [GetSO] PlayerData playerData;
    private void Start()
    {
        this.InjectGetSO();
        playerData.SubscribeRespawn(() => AudioManager.Instance.Play("Kuolema"));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //yksinkertaistetusti, toki efektit pitää lisätä action kutsulla esim playerdsatan kautta
            playerData.Respawn();
        }
    }
}
