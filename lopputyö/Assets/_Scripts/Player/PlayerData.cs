﻿using System;
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

    Action respawn;

    Action addRoomChangeVelocity;

    public void SubscribeTeleport(Action<Vector2> _action)
    {
        teleport += _action;
    }
    public void UnSubscribeTeleport(Action<Vector2> _action)
    {
        teleport -= _action;
    }

    public void SubscribeRespawn(Action _action)
    {
        respawn += _action;
    }
    public void UnSubscribeRespawn(Action _action)
    {
        respawn -= _action;
    }

    public void SubscribeAddRoomChangeVelocity(Action _action)
    {
        addRoomChangeVelocity += _action;
    }
    public void UnSubscribeAddRoomChangeVelocity(Action _action)
    {
        addRoomChangeVelocity -= _action;
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

    public void AddRoomChangeVelocity()
    {
        addRoomChangeVelocity.Invoke();
    }

    public void Respawn()
    {
        respawn.Invoke();
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

        respawn = delegate { };
        Helpers.AddAutounsubDelegate(() => respawn = null);

        addRoomChangeVelocity = delegate { };
        Helpers.AddAutounsubDelegate(() => addRoomChangeVelocity = null);


    }

    protected override void InitSO(ScriptableObject _obj)
    {
        PlayerData _oldData = _obj as PlayerData;


        positionPlaced = _oldData.PositionPlaced;
        position = _oldData.Position;
        teleportRoom = _oldData.TeleportRoom;

        respawnPoint = _oldData.RespawnPoint;
    }
}

