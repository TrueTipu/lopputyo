using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
[System.Serializable]
public class PlayerData : PlaytimeObject
{
    bool positionPlaced;

    [SerializeField]Vector3 position;
    public Vector3 Position { get; private set; }

    [SerializeField] Vector3 respawnPoint = Vector2.zero;
    public Vector3 RespawnPoint { get; private set; }
    
    public void TrySetPosition(Vector3 _position)
    {
        if (!positionPlaced)
        {
            positionPlaced = true;
            position = _position;
            Position = position;
        }
    }

    public void SetRespawnPoint(Vector2 _pos)
    {
        RespawnPoint = _pos;
    }



    public void UpdatePosition(Vector3 _position)
    {
        Position = _position;
    }

    protected override void LoadInspectorData()
    {
        Position = position;
        positionPlaced = false;

        RespawnPoint = respawnPoint;
    }
}

