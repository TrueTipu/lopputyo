using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class SceneLoader : PersistentSingleton<SceneLoader>
{


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene(0);
        }
    }
}
