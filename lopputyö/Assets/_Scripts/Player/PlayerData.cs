using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
[System.Serializable]
public class PlayerData : PlaytimeObject, IHasDelegates
{
    [SerializeField] bool positionPlaced = false;
    bool PositionPlaced { get; set; }

    [SerializeField]Vector3 position;
    public Vector3 Position { get; private set; }

    [SerializeField] Vector3 respawnPoint = Vector2.zero;
    public Vector3 RespawnPoint { get; private set; }

    [SerializeField] Vector2Int teleportRoom;
    public Vector2Int TeleportRoom { get; private set; }

    Action<Vector2> teleport;

    public void SubscribeTeleport(Action<Vector2> _action)
    {
        teleport += _action;
    }
    public void UnSubscribeTeleport(Action<Vector2> _action)
    {
        teleport -= _action;
    }

    protected override void OnEnable()
    {
        base.OnEnable(); 
    }

    public void TrySetPosition(Vector3 _position)
    {
        if (!PositionPlaced)
        {
            PositionPlaced = true;
            position = _position;
            Position = position;
        }
    }

    public void SetRespawnPoint(Vector2 _pos)
    {
        RespawnPoint = _pos;
    }

    public void Teleport(Vector3 _position)
    {
        teleport.Invoke(_position);
    }

    public void UpdatePosition(Vector3 _position)
    {
        Position = _position;
    }

    public void ChangeTeleportLocation(Vector2Int _cords)
    {
        TeleportRoom = _cords;
    }

    protected override void LoadInspectorData()
    {
        PositionPlaced = positionPlaced;
        Position = position;
        TeleportRoom = teleportRoom;

        RespawnPoint = respawnPoint;
    }

    public void AutoUnsubscribeDelegates()
    {
        teleport = delegate { };
        Helpers.AddAutounsubDelegate(() => teleport = null);
    }
}

