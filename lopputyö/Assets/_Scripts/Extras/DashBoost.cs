using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBoost : MonoBehaviour
{
    bool active = true;

    SpriteRenderer spriteRenderer;

    [SerializeField] float time;
    [SerializeField] Sprite sprite1;
    [SerializeField] Sprite sprite2;
    [SerializeField] GameObject particles;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        active = true;
    }

    public IEnumerator DestroySelf()
    {
        active = false;
        spriteRenderer.sprite = sprite2;
        Instantiate(particles, transform);

        yield return new WaitForSeconds(time);

        spriteRenderer.sprite = sprite1;
        active = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (active && collision.CompareTag("Player"))
        {
            if (collision.GetComponent<PlayerMovement>().GetState.IsDashing)
            {
                collision.GetComponent<PlayerMovement>().GetState.DashBoostActivationCall();
                StartCoroutine(DestroySelf());
            }
        }
    }
}
