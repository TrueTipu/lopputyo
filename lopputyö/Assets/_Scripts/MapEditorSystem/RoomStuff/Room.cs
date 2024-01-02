using System;
using System.Collections.Generic;
using UnityEngine;
using Array2DEditor;
using System.Collections;



[CreateAssetMenu(fileName = "Room", menuName = "ScriptableObjects/Room")]
[System.Serializable]
public class Room : ScriptableObject
{
    [SerializeField] Sprite mapIcon;
    public Sprite MapIcon => mapIcon;

    [GetSO] RoomVisitedData roomVisitedData;

    [SerializeField] GameObject roomPrefab;
    public GameObject RoomPrefab => roomPrefab;

    [SerializeField] new private string name;
    public string Name => name;

    [SerializeField] RoomOpenData possibleDirections = new RoomOpenData();
    public RoomOpenData PossibleDirections => possibleDirections;

    [SerializeField] bool isMovable = true;
    public bool IsMovable => (isMovable && !HasCore);

    public bool HasCore => Core != null;
    [SerializeField] CoreData core;
    public CoreData Core => core;




    public Dictionary<Direction, bool> BlockedDirections { get; } = new Dictionary<Direction, bool>()
    {
        {Direction.Up, false},
        {Direction.Right, false},
        {Direction.Left, false},
        {Direction.Down, false},
        {Direction.UpLeft, false},
        {Direction.RightDown, false},
        {Direction.LeftDown, false},
        {Direction.DownLeft, false},
        {Direction.UpRight, false},
        {Direction.RightUp, false},
        {Direction.LeftUp, false},
        {Direction.DownRight, false},
    };

    public Direction BlockedExtraDirection;


    public void LoadRoom()
    {
        this.InjectGetSO();
        Dictionary<int, Direction> dirs = new Dictionary<int, Direction>
        {
            {0, Direction.Left},
            {1, Direction.Left},
            {2, Direction.Left},
            {3, Direction.Up},
            {4, Direction.Up},
            {5, Direction.Up},
            {6, Direction.Right},
            {7, Direction.Right},
            {8, Direction.Right},
            {9, Direction.Down},
            {10, Direction.Down},
            {11, Direction.Down},
        };

        Core?.SubscribeSetAbility((_newAbility, _oldAbility) => {
            if (_newAbility != PlayerAbility.None)
            {
                BlockedExtraDirection = dirs[roomVisitedData.RoomsVisited[roomVisitedData.RoomsVisited.Count - 1].EnterPointIndexes[0]];
            }
            else
            {
                BlockedExtraDirection = Direction.None;
            }
        });
    }

    public void AddBlockedDirection(Direction _dir)
    {
        BlockedDirections[_dir] = true;
    }
    public void RemoveBlockedDirection(Direction _dir)
    {
        BlockedDirections[_dir] = false;
    }
   
}
