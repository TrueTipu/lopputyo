using UnityEngine;
using System.Collections;
using System;
public class RoomSpawner : MonoBehaviour
{

    Room room;
    RoomObject roomObject = null;
    public bool IsActive { get { return roomObject == null ? false : roomObject.gameObject.activeSelf; } }

    Vector2Int tileCords;


    Action<Vector2Int> roomActivated;

    public void InitRoomSpawn(Room _room, int _x, int _y, Action<Vector2Int> _callBackListener)
    {
        if (_room == null) return;
        room = _room;
        roomObject = Instantiate(_room.RoomPrefab, transform).GetComponent<RoomObject>();
        tileCords = new Vector2Int(_x, _y);

        SetBlocks();

        roomActivated = _callBackListener;
    }

    public void ActivateRoom()
    {
        if (roomObject == null) return;
        //jos ei loadata koko sceneä
        //if (!updaited)
        //{


        //room = _room;
        //UpdateRoomSpawn();
        //updaited = true;
        //}
        roomObject.gameObject.SetActive(true);
        roomObject.SetActive(true);
    }

    public void DisableRoom()
    {
        if (roomObject == null) return;

        roomObject.SetActive(false);
    }

    //Jos ei loadata koko sceneä

    //void UpdateRoomSpawn()
    //{
    //    roomObject = transformshenanigans
    //    SetBlocks();
    //}

    public void SetName(string _name)
    {
        name = _name;
    }

    void SetBlocks()
    {
        roomObject.Clear();
        foreach (Direction _bDir in room.BlockedDirections.Keys)
        {
            if (room.BlockedDirections[_bDir] == true)
            {
                roomObject.BlockPath(_bDir);
            }
                
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            roomActivated(tileCords);
        }
    }
}
