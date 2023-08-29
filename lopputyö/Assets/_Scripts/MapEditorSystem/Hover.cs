using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/// <summary>
///Hallitsee hiirtä, hoveria ja koko gridin ja room managerin yhteyttä
///TODO: jos jaksat ja haluat siirrä tähän vain position ja input check, ja itse toiminnot omaan yhtenäisyysmanageriin
/// </summary>
public class Hover : MonoBehaviour
{
    [GetSO] LevelTileGridData grid;
    RoomManager roomManager;

    Vector3 rememberPos;
    Tile rememberTile;
    Tile currentTile;

    Tile selectedTile;

    [SerializeField] GameObject selectedLogo;

    private void Start()
    {
        this.InjectGetSO();
        roomManager = RoomManager.Instance;
    }

    private void Update()
    {
        MouseCheck();
        CheckKeyboard();
        if(Input.GetKeyDown(KeyCode.Z) || Input.GetMouseButtonDown(0))
        {
            SelectTile();
        }
        if(Input.GetKeyDown(KeyCode.X) || Input.GetMouseButtonDown(1))
        {
            selectedTile = null;
            selectedLogo.SetActive(false);
        }

        transform.position = currentTile.transform.position;

    }

    void SelectTile()
    {
        if (selectedTile == null)
        {
            if (!currentTile.HasRoom) return;

            //huom, käsitellään vain tilejä, koska ainakin joskus ajattelin että tämä johtaisi siistimpään koodiin
            //muuta järkevästi jos muutat, idea on siis että roomien siirtymiä hallitaan yhdestä(1) paikasta: room manager
            //ei sekotetat gridiä tähän
            selectedTile = currentTile;

            selectedLogo.SetActive(true);
            selectedLogo.transform.position = selectedTile.transform.position;
            //Debug.Log("Tile Selected");
        }
        else
        {
            if (currentTile.HasRoom)
            {
                //Debug.Log("Täynnä");
                return;
            }
            if (!selectedTile.HasMovableRoom)
            {
                //et voi siirtää tätä huonetta
                selectedTile = null;
                return;
            }

            roomManager.MoveRoom(selectedTile.TileCords, currentTile.TileCords);
            selectedTile = null;

            selectedLogo.SetActive(false);
            //Debug.Log("Tile Placed");
        }

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
