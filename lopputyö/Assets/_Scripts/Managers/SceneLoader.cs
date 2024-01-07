using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.SceneManagement;


public class SceneLoader : PersistentSingleton<SceneLoader>
{
    [GetSO] RoomSet roomset;
    protected override void Awake()
    {
        ResourceSystem.AssembleResources();
        base.Awake();




    }

    private void Start()
    {
        this.InjectGetSO();

        roomset.Rooms.Values.ToList().ForEach((r) => r.ResetBlockedExtraDir());
    }
    private void OnEnable()
    {
        Resources.FindObjectsOfTypeAll<PlaytimeObject>().ToList().ForEach(x => x.OnStartLoad());

    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    LoadLevelEditor();
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    SceneManager.LoadScene(0);
        //}
    }



    public static void ChangeScene(int _index)
    {
        if(_index == 0)
        {
            SceneManager.LoadScene("LevelTest");
        }
        if (_index == 1)
        {
            SceneManager.LoadScene("LevelEditor");
        }
        if (_index == 2)
        {
            SceneManager.LoadScene("End Sequence");
        }
    }
    public static void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public static void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
