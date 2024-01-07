using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKRIPTI : MonoBehaviour
{
    [SerializeField] PlayerMovement pelaaja;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        pelaaja.transform.position = this.transform.position;
    }

    void Update()
    {
        
    }
}
