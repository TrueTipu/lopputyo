using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneLoader.ChangeScene(2);
        }
    }

}
