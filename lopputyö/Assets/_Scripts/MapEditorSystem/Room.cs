using System;
using System.Collections.Generic;
using UnityEngine;
using Array2DEditor;


[CreateAssetMenu(fileName = "Room", menuName = "ScriptableObjects/Room")]
[System.Serializable]
public class Room : ScriptableObject
{

    [field: SerializeField] public Sprite MapIcon { get; private set; }

    [field: SerializeField] public GameObject RoomPrefab { get; private set; }

    [field: SerializeField] public string Name { get; private set; }

    public DirectionDictionary PossibleDirections { get; private set; } = new DirectionDictionary()   
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

    [SerializeField] Array2DBool possibleDirectionsSetter;


    Dictionary<Direction, bool> blockedDirections = new Dictionary<Direction, bool>()
    {
        {Direction.Up, false},
        {Direction.Right, false},
        {Direction.Left, false},
        {Direction.Down, false},
    };

    public Dictionary<Direction, bool> BlockedDirections
    { get { return new Dictionary<Direction, bool>(blockedDirections); } }
    

    public void AddBlockedDirection(Direction _dir)
    {
        blockedDirections[_dir] = true;
    }
    public void RemoveBlockedDirection(Direction _dir)
    {
        blockedDirections[_dir] = false;
    }

}

public enum Direction
{
    Left,
    LeftUp,
    LeftDown,
    Right,
    RightUp,
    RightDown,
    Up,
    UpLeft,
    UpRight,
    Down,
    DownLeft,
    DownRight,
}