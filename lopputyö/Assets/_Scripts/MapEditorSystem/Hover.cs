using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/// <summary>
///Hallitsee hiirtä, hoveria ja koko gridin ja room managerin yhteyttä
///joo parmemal mallil
///also tp menee tälle nyt lol
/// </summary>
public class Hover : MonoBehaviour
{
    [GetSO] LevelTileGridData grid;
    [GetSO] PlayerData playerData;
    [GetSO] ActiveStreamsData streamsData;
    RoomManager roomManager;

    Vector3 rememberPos;
    Tile rememberTile;
    Tile currentTile;

    Tile selectedTile;

    [SerializeField] GameObject selectedLogo;


    [GetSO] RoomSet roomSet; //VAIHDA MYÖHEMMIN, vain koska next room

    private void Start()
    {
        this.InjectGetSO();
        roomManager = RoomManager.Instance;
    }

    private void Update()
    {
        if (Keys.DashKeysDown() && !Input.GetMouseButtonDown(1))
        {
            Helpers.Camera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
            rememberPos = Helpers.GetMousePosition();
        }
        else
        {
            MouseCheck();
        }
        CheckKeyboard();
    
        if (Keys.JumpKeysDown() && !Keys.DirectionKey(Direction.Up))
        {
            SelectTile();
        }

        if (Keys.InteractKeysDown())
        {
            selectedTile = null;
            selectedLogo.SetActive(false);
        }

        if (Keys.JumpKeysDown() && Keys.DashKeys() && !selectedTile.HasMovableRoom)
        {
            var _core = selectedTile.Core;
            if (_core == null) return;

            if(streamsData.GetLink(grid.GetTile(playerData.TeleportRoom).Core, _core))
            {
                playerData.ChangeTeleportLocation(selectedTile.TileCords);
                StartCoroutine(Helpers.PointO_OneSDelay(() => SceneLoader.ChangeScene(0)));

                selectedTile = null;
                selectedLogo.SetActive(false);
            }



        }


        //if (Input.GetKeyDown(KeyCode.P))//DEBUG
        //{
        //    roomSet.IncreaseStreamLevel(); 
        //}

        transform.position = currentTile.transform.position;

    }

    void SelectTile()
    {
        if(roomSet.GetNextRooms().Count > 0)
        {
            selectedTile = null;
            selectedLogo.SetActive(false);

            if (currentTile.HasRoom)
            {
                return;
            }

            roomManager.MoveRoom(roomSet.TakeRoom(), currentTile.TileCords);
            return;
        }

        if (selectedTile == null)
        {


            if (!currentTile.HasRoom) return;

            AudioManager.Instance.Play("Asetus");
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
            if (currentTile.HasRoom && !(!selectedTile.HasMovableRoom || selectedTile.StreamLocked))
            {
                AudioManager.Instance.PlayRandomPitch("Siirto");
                //Debug.Log("Täynnä");
                return;
            }
            else if (currentTile.HasRoom)
            {
                AudioManager.Instance.PlayRandomPitch("Siirto");
                //et voi siirtää tätä huonetta
                selectedTile = null;
                SelectTile();
                return;
            }
            if (!selectedTile.HasMovableRoom || selectedTile.StreamLocked)
            {
                AudioManager.Instance.PlayRandomPitch("Siirto");
                //et voi siirtää tätä huonetta
                selectedTile = null;
                return;
            }

            AudioManager.Instance.Play("Asetus");
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
        if (Keys.DirectionKeyDown(Direction.Up))
        {
            currentTile = grid.GetTile(currentTile.TileCords.x, currentTile.TileCords.y + 1);
        }
        if (Keys.DirectionKeyDown(Direction.Down))
        {
            currentTile = grid.GetTile(currentTile.TileCords.x, currentTile.TileCords.y - 1);
        }
        if (Keys.DirectionKeyDown(Direction.Right))
        {
            currentTile = grid.GetTile(currentTile.TileCords.x + 1, currentTile.TileCords.y);
        }
        if (Keys.DirectionKeyDown(Direction.Left))
        {
            currentTile = grid.GetTile(currentTile.TileCords.x - 1, currentTile.TileCords.y);
        }
    }

}
