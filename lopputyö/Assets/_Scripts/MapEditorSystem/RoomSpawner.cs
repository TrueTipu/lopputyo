using UnityEngine;
using System.Collections;
using System;
[RequireComponent(typeof(BoxCollider2D))]
public class RoomSpawner : MonoBehaviour, ITileNode
{
    [GetSO] RoomVisitedData roomVisitedData;

    RoomSpawnerGrid spawnerGrid;

    Room room;
    public RoomObject RoomObject { get; private set; } = null;
    public bool IsActive { get { return RoomObject == null ? false : RoomObject.gameObject.activeSelf; } }

    Vector2Int tileCords;

    Action<Vector2Int, Vector2> roomActivated = delegate { };

    void OnEnable()
    {
        this.InjectGetSO();
    }

    private IEnumerator Start()
    {
        yield return null;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public void InitRoomSpawn(Room _room, int _x, int _y, RoomSpawnerGrid _spawner, Action<Vector2Int, Vector2> _callBackListener)
    {
        if (_room == null) return;
        room = _room;
        spawnerGrid = _spawner;
        RoomObject = Instantiate(_room.RoomPrefab, transform).GetComponent<RoomObject>();
        tileCords = new Vector2Int(_x, _y);

        SetBlocks();

        roomActivated += _callBackListener;
        roomActivated += (Vector2Int _cords, Vector2 _pos) => roomVisitedData.AddRoom(RoomObject, spawnerGrid, tileCords, _pos);
    }


    public void ActivateDisabledRoom()
    {
        if (RoomObject == null) return;


        ///jos ei loadata koko sceneä
        //if (!updaited)
        //{


        //room = _room;
        //UpdateRoomSpawn();
        //updaited = true;
        //}


        RoomObject.gameObject.SetActive(true);
        RoomObject.SetActive(true);
    }

    public void DisableActiveRoom()
    {
        if (RoomObject == null) return;

        RoomObject.SetActive(false);
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
        RoomObject.Clear();
        foreach (Direction _bDir in room.BlockedDirections.Keys)
        {
            if (room.BlockedDirections[_bDir] == true)
            {
                RoomObject.BlockPath(_bDir);
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
