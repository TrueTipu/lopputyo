using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LockRoom : MonoBehaviour
{
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
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("End Screen");
    }
}
