using UnityEngine;
using System.Collections;

public class PlayerTP : MonoBehaviour
{

    [GetSO] PlayerData playerData;
    [GetSO] RoomSpawnerGridData gridData;

    // Use this for initialization
    IEnumerator Start()
    {
        this.InjectGetSO();

        yield return null;
        
        var _spawner = gridData.GetTile(playerData.TeleportRoom);
        if (gridData.GetTile(transform.position) != _spawner)
        {
            playerData.Teleport(_spawner.transform.position);
        }
    }

}
