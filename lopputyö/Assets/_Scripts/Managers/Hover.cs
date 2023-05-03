using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/// <summary>
///Hallitsee hiirtä ja hoveria
/// </summary>
public class Hover : MonoBehaviour
{
    LevelGridManager grid;

    Vector3 rememberPos;
    Tile rememberTile;
    Tile currentTile;

    private void Start()
    {
        grid = LevelGridManager.Instance;
    }

    private void Update()
    {
        MouseCheck();
        CheckKeyboard();

        transform.position = currentTile.transform.position;

    }

    /// <returns> Palauttaa true/false sen mukaan vaihtuiko ruutu </returns>
    void MouseCheck()
    {
        Vector3 _mousePos = Helpers.GetMousePosition();
        if (_mousePos == rememberPos)
        {
            return;
        }
        else { rememberPos = _mousePos; }

        currentTile = grid.GetTile(_mousePos);
    }

    void CheckKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentTile = grid.GetTile(currentTile.TileCords.x, currentTile.TileCords.y + 1);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentTile = grid.GetTile(currentTile.TileCords.x, currentTile.TileCords.y - 1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentTile = grid.GetTile(currentTile.TileCords.x + 1, currentTile.TileCords.y);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentTile = grid.GetTile(currentTile.TileCords.x - 1, currentTile.TileCords.y);
        }
    }

}
