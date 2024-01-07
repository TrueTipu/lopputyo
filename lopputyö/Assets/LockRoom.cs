using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LockRoom : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer rend;
    [SerializeField] Sprite sprit;
    [SerializeField] Transform _poos;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Helpers.Camera.GetComponentInParent<CameraMovement>().SetTarget(_poos);
            StartCoroutine(End());
        }
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(4);
        rend.sprite = sprit;
        AudioManager.Instance.Play("Tömähdys");
        yield return new WaitForSeconds(3);
        anim.SetTrigger("trigger");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("End Screen");
    }
}
