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
            LoadLevelEditor();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene(0);
        }
    }

    public static void LoadLevelEditor()
    {
        SceneManager.LoadScene(1);
    }

    public void ChangeScene(int _index)
    {
        SceneManager.LoadScene(_index);
    }
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
