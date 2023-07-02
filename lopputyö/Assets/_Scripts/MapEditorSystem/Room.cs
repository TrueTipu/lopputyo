using System;
using System.Collections.Generic;
using UnityEngine;
using Array2DEditor;
using System.Collections;



[CreateAssetMenu(fileName = "Room", menuName = "ScriptableObjects/Room")]
[System.Serializable]
public class Room : ScriptableObject
{

    [field: SerializeField] public Sprite MapIcon { get; private set; }

    [field: SerializeField] public GameObject RoomPrefab { get; private set; }

    [field: SerializeField] public string Name { get; private set; }

    [SerializeField] RoomOpenData possibleDirections = new RoomOpenData();
    public RoomOpenData PossibleDirections => possibleDirections;

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


    public void AddBlockedDirection(Direction _dir)
    {
        BlockedDirections[_dir] = true;
    }
    public void RemoveBlockedDirection(Direction _dir)
    {
        BlockedDirections[_dir] = false;
    }
   
}
