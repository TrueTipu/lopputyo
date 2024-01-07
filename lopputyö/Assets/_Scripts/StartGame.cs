using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] Animator anim;

    public void PlayGame ()
    {
        StartCoroutine(StartTransition());
    }

    public void QuitGame ()
    {
        Application.Quit();
    }

    IEnumerator StartTransition () 
    {
        AudioManager.Instance.Play("Enkeli1");
        anim.SetTrigger("start");
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
