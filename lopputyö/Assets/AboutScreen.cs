using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutScreen : MonoBehaviour
{
    [SerializeField] Animator anim;

    public void AboutAnim(string trigg)
    {
        AudioManager.Instance.Play("Enkeli4");
        anim.SetTrigger(trigg);    
    }
}
