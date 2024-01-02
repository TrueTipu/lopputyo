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
        
        RoomSpawner _spawner = gridData.GetTile(playerData.TeleportRoom);
        if (gridData.GetTile(transform.position) != _spawner)
        {
            playerData.Teleport(_spawner.RoomObject.SpawnPoint);
        }
    }

}
