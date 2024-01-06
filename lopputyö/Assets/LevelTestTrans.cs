using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTestTrans : MonoBehaviour
{
    [SerializeField] Animator anim;
    bool started;

    void Awake() 
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Update()
    {
        if (!started && SceneManager.GetActiveScene().name == "LevelTest"){
            started = true;
            Debug.Log("test");
            anim.SetTrigger("Start");
            Destroy(this.gameObject, 2f);
        }        
    }

}
