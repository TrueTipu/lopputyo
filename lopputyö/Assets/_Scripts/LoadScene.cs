using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    //[SerializeField] string sceneName;
    // Start is called before the first frame update


    public void SceneLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);       
    }

}
