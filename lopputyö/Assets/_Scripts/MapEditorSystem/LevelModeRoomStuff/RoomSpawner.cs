using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
[RequireComponent(typeof(BoxCollider2D))]
public class RoomSpawner : MonoBehaviour, ITileNode
{
    [GetSO] RoomVisitedData roomVisitedData;
    [GetSO] PlayerData playerData;

    public RoomObject RoomObject { get; private set; } = null;
    public bool IsActive { get { return RoomObject == null ? false : RoomObject.gameObject.activeSelf; } }

    Action<Vector2> roomActivated = delegate { };

    public Vector2Int TileCords { get; private set; }

    void OnEnable()
    {
        this.InjectGetSO();
    }

    private IEnumerator Start()
    {
        yield return null;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public void InitRoomSpawn(Room _room, int _x, int _y, Action<Vector2> _activationCallBack)
    {
        if (_room == null) return;
        RoomObject = Instantiate(_room.RoomPrefab, transform).GetComponent<RoomObject>();
        TileCords = new Vector2Int(_x, _y);

        if (_room.HasCore) _room.Core.SetRoomPos(TileCords);

        SetBlocks(_room);

        roomActivated += _activationCallBack;
        roomActivated += (_pos) => roomVisitedData.AddRoom(RoomObject, TileCords, _pos);

        roomActivated += (_pos) =>
        {
            if (Enumerable.Range(9, 3).Contains(RoomObject.GetClosest(_pos))) //jos joku alaosista eli 9, 10 tai 11
            {
                playerData.AddRoomChangeVelocity();
            }
        };
        
        if (_room.HasCore)
        {
            roomActivated += (_pos) => { if (_room.Core.Powered) roomVisitedData.ResetVisits(TileCords); };

            _room.Core.SubscribeSetAbility((_p1, _p2) => StartCoroutine(Helpers.PointOneSDelay(() => SetBlocks(_room))));
        }

        roomActivated += (_pos) => SetSpawnPoint(_pos, RoomObject);

    }


    void SetSpawnPoint(Vector2 _pos, RoomObject _roomObject)
    {
        playerData.SetRespawnPoint(_roomObject.GetRespawnPoint(_pos));
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

    ///kinda vois siirtää roomObjektiin mut toimikoon täällä
    public void SetBlocks(Room _room)
    {

        RoomObject.Clear();
        foreach (Direction _bDir in _room.BlockedDirections.Keys)
        {
            if (_room.BlockedDirections[_bDir] == true)
            {
                RoomObject.BlockPath(_bDir);
            }
        }

        switch (_room.BlockedExtraDirection)
        {
            case Direction.Left:
                RoomObject.BlockPath(Direction.LeftDown);
                RoomObject.BlockPath(Direction.Left);
                RoomObject.BlockPath(Direction.LeftUp);
                break;
            case Direction.Right:
                RoomObject.BlockPath(Direction.RightDown);
                RoomObject.BlockPath(Direction.Right);
                RoomObject.BlockPath(Direction.RightUp);
                break;
            case Direction.Up:
                RoomObject.BlockPath(Direction.UpLeft);
                RoomObject.BlockPath(Direction.Up);
                RoomObject.BlockPath(Direction.UpRight);
                break;
            case Direction.Down:
                RoomObject.BlockPath(Direction.DownLeft);
                RoomObject.BlockPath(Direction.Down);
                RoomObject.BlockPath(Direction.DownRight);
                break;
            case Direction.None:
                break;
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            roomActivated(collision.transform.position);
        }
    }
}
