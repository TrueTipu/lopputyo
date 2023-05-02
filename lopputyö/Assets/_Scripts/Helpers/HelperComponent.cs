using UnityEngine;
using System.Collections;

public class HelperComponent : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        Helpers.Tick(Time.deltaTime);
    }
}
