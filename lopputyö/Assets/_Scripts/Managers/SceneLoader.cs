﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.SceneManagement;


public class SceneLoader : PersistentSingleton<SceneLoader>
{
    protected override void Awake()
    {
        base.Awake();
        ResourceSystem.AssembleResources();
    }
    private void OnEnable()
    {
        Resources.FindObjectsOfTypeAll<PlaytimeObject>().ToList().ForEach(x => x.OnStartLoad());
    }

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

    public static void LoadLevel()
    {
        SceneManager.LoadScene(0);
    }

    public static void ChangeScene(int _index)
    {
        SceneManager.LoadScene(_index);
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
