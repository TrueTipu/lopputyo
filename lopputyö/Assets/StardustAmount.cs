using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StardustAmount : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text.text = "Startdust found: " + CollectibleManager.Instance.collectiblesCollected.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
