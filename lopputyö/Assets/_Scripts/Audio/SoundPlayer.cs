using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    AudioSource source;
    [SerializeField] float speed = 1;


    void Start()
    {
        Init();
    }

    void Init()
    {
        source = AudioManager.Instance.PlayOnLoop("Spiders");
        StartCoroutine(SpiderSound());
    }

    private IEnumerator SpiderSound()
    {
        while (true)
        {
            int _r = Random.Range(0, 100);
            Debug.Log(_r);
            if (_r > 90)
            {
                while (source.volume < 0.4)
                {
                    source.volume += speed * Time.deltaTime;
                    yield return null;
                }
                while (source.volume > 0)
                {
                    source.volume -= speed * Time.deltaTime;
                    yield return null;
                }
            }
            yield return new WaitForSeconds(20);
        }

    }




}
