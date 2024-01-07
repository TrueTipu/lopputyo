using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] Animator anim;
    
    void OnTriggerEnter2D (Collider2D other){
        if (other.CompareTag("Player")) {
            anim.SetTrigger("show");
        }
    }

    void OnTriggerExit2D (Collider2D other){
        if (other.CompareTag("Player")) {
            anim.SetTrigger("hide");
        }
    }
}
