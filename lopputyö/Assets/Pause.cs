using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    private bool paused;
    [SerializeField] GameObject pausemenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused && Keys.EscapeKeysDown()){
            Time.timeScale = 0f;
            pausemenu.SetActive(true);
        }    
        else if (paused && Keys.EscapeKeysDown())
        {
            Continue();
        }
    }

    public void Continue() {
        Time.timeScale = 1f;
        pausemenu.SetActive(false);
    }
}
