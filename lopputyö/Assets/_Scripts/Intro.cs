using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sRenderer;
    [SerializeField] Sprite[] sprites;
    private int knocks;

    [SerializeField] private GameObject img;
    [SerializeField] private GameObject platform;
    [SerializeField] private GameObject eff;
    [SerializeField] Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim.SetTrigger("end");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && knocks < 11)
        {
            knocks++;
            Instantiate(eff, transform.position, Quaternion.identity);
        }

        if (knocks == 3){
            sRenderer.sprite = sprites[0];
            img.SetActive(false);
        }
        else if (knocks == 6){
            sRenderer.sprite = sprites[1];
        }
        else if (knocks == 11) {
            sRenderer.sprite = sprites[2];
            platform.SetActive(false);
            StartCoroutine(NextScene());
        }
    }

    IEnumerator NextScene(){
        yield return new WaitForSeconds(1.5f);
        anim.SetTrigger("start");
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
