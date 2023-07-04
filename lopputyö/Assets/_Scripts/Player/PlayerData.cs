using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
[System.Serializable]
public class PlayerData : PlaytimeObject
{
    bool positionPlaced;
    Vector3 position;

    public Vector3 Position { get; set; }
    
    public void TrySetPosition(Vector3 _position)
    {
        if (!positionPlaced)
        {
            positionPlaced = true;
            position = _position;
            Position = position;
        }
    }

    protected override void LoadInspectorData()
    {
        Position = position;
        positionPlaced = false;
    }
}

