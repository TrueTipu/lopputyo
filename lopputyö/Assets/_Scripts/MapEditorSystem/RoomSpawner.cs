using UnityEngine;
using System.Collections;
using System;
public class RoomSpawner : MonoBehaviour, ITileNode
{
    [GetSO] RoomVisitedData roomVisitedData;

    Room room;
    RoomObject roomObject = null;
    public bool IsActive { get { return roomObject == null ? false : roomObject.gameObject.activeSelf; } }

    Vector2Int tileCords;

    Action<Vector2Int, Vector2> roomActivated = delegate { };

    void OnEnable()
    {
        this.InjectGetSO();
    }

    public void InitRoomSpawn(Room _room, int _x, int _y, Action<Vector2Int, Vector2> _callBackListener)
    {
        if (_room == null) return;
        room = _room;
        roomObject = Instantiate(_room.RoomPrefab, transform).GetComponent<RoomObject>();
        tileCords = new Vector2Int(_x, _y);

        SetBlocks();
        roomObject.PathNodes.ResetLocalDatas();

        roomActivated += _callBackListener;
        roomActivated += (Vector2Int _cords, Vector2 _pos) => roomVisitedData.AddRoom(roomObject, _pos);
    }


    public void ActivateRoom()
    {
        if (roomObject == null) return;
        ///jos ei loadata koko sceneä
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
            roomActivated(tileCords, collision.transform.position);
        }
    }

}
