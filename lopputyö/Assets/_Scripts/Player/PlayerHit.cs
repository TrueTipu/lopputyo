using UnityEngine;
using System.Collections;

public class PlayerHit : MonoBehaviour
{
    [GetSO] PlayerData playerData;
    [SerializeField] GameObject eff;

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
            //lmao lisään efektit täällä koska kuka oikeesti huomaa eron
            Instantiate(eff, transform.position, Quaternion.identity);
            playerData.Respawn();
        }
    }
}
