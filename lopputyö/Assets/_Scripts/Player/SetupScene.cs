using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupScene : MonoBehaviour
{
    [GetSO] PlayerData playerData;
    void Start()
    {
        this.InjectGetSO();
        playerData.SetRespawnPoint(transform.position);
    }

}
