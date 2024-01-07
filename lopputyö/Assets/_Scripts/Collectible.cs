using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] GameObject eff;

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.Play("Launch");
            Instantiate(eff, transform.position, Quaternion.identity);
            CollectibleManager.Instance.Collect();
            Destroy(this.gameObject);
        }
    }
}

