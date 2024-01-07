using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : Singleton<MusicPlayer>
{
    private void Update()
    {
        int _a = SceneManager.GetActiveScene().buildIndex;

        switch (_a)
        {
            case 0:
                AudioManager.Instance.PlayOnLoop("Menu");
                AudioManager.Instance.Stop("Intro");
                AudioManager.Instance.Stop("Seikkailu");
                AudioManager.Instance.Stop("Loppu");
                AudioManager.Instance.Stop("Credits");
                break;
            case 1:
                AudioManager.Instance.PlayOnLoop("Intro");
                AudioManager.Instance.Stop("Menu");
                AudioManager.Instance.Stop("Seikkailu");
                AudioManager.Instance.Stop("Loppu");
                AudioManager.Instance.Stop("Credits");
                break;
            case 2:
                AudioManager.Instance.PlayOnLoop("Seikkailu");
                AudioManager.Instance.Stop("Menu");
                AudioManager.Instance.Stop("Intro");
                AudioManager.Instance.Stop("Loppu");
                AudioManager.Instance.Stop("Credits");
                break;
            case 3:
                AudioManager.Instance.PlayOnLoop("Seikkailu");
                AudioManager.Instance.Stop("Menu");
                AudioManager.Instance.Stop("Intro");
                AudioManager.Instance.Stop("Loppu");
                AudioManager.Instance.Stop("Credits");
                break;
            case 4:
                AudioManager.Instance.PlayOnLoop("Loppu");
                AudioManager.Instance.Stop("Menu");
                AudioManager.Instance.Stop("Intro");
                AudioManager.Instance.Stop("Seikkailu");
                AudioManager.Instance.Stop("Credits");
                break;
            case 5:
                AudioManager.Instance.PlayOnLoop("Credits");
                AudioManager.Instance.Stop("Menu");
                AudioManager.Instance.Stop("Intro");
                AudioManager.Instance.Stop("Seikkailu");
                AudioManager.Instance.Stop("Loppu");
                break;
            default:
                break;
        }
    }

}