using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class Keys
{
    //helpotetaan nappien vaihtamista ja ehkä toiseen input järjestelmään siirtymistä


    #region jump
    public static bool JumpKeysDown()
    {
        return (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Z));
    }
    public static bool JumpKeys()
    {
        return (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0) || Input.GetKey(KeyCode.Z));
    }
    public static bool JumpKeysUp()
    {
        return (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Z));
    }
    #endregion

    #region dash
    public static bool DashKeysDown()
    {
        return (Input.GetKeyDown(KeyCode.C) || Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.LeftShift));
    }
    public static bool DashKeys()
    {
        return (Input.GetKey(KeyCode.C) || Input.GetMouseButton(1) || Input.GetKey(KeyCode.LeftShift));
    }
    public static bool DashKeysUp()
    {
        return (Input.GetKeyUp(KeyCode.C) || Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.LeftShift));
    }
    #endregion



    #region Interact
    public static bool InteractKeysDown()
    {
        return (Input.GetKeyDown(KeyCode.I) || Input.GetMouseButtonDown(2) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.X));
    }
    public static bool InteractKeys()
    {
        return (Input.GetKey(KeyCode.I) || Input.GetMouseButton(2) || Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.X));
    }
    public static bool InteractKeysUp()
    {
        return (Input.GetKeyUp(KeyCode.I) || Input.GetMouseButtonUp(2) || Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.X));
    }
    #endregion

    #region Escape
    public static bool EscapeKeysDown()
    {
        return (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return));
    }
    public static bool EscapeKeys()
    {
        return (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Return));
    }
    public static bool EscapeKeysUp()
    {
        return (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Return));
    }
    #endregion

    static Dictionary<Direction, List<KeyCode>> valuePairs = new Dictionary<Direction, List<KeyCode>>()
    {
        {Direction.Left,  new List<KeyCode>(){KeyCode.LeftArrow, KeyCode.A, } },
        {Direction.Right,  new List<KeyCode>(){KeyCode.RightArrow, KeyCode.D, } },
        {Direction.Up,  new List<KeyCode>(){KeyCode.UpArrow, KeyCode.W,  } },
        {Direction.Down,  new List<KeyCode>(){KeyCode.DownArrow, KeyCode.S,  } },
    };

    #region Direction
    public static bool DirectionKeyDown(Direction _dir)
    {
        return (valuePairs[_dir].Any((x) => Input.GetKeyDown(x)));
    }
    public static bool DirectionKey(Direction _dir)
    {
        return (valuePairs[_dir].Any((x) => Input.GetKey(x)));
    }
    public static bool IDirectionKeyÚp(Direction _dir)
    {
        return (valuePairs[_dir].Any((x) => Input.GetKeyUp(x)));
    }
    #endregion

    public static Direction GetInputDirection()
    {
        if(DirectionKey(Direction.Left))
        {
            return Direction.Left;
        }
        if (DirectionKey(Direction.Right))
        {
            return Direction.Right;
        }
        if (DirectionKey(Direction.Up))
        {
            return Direction.Up;
        }
        if (DirectionKey(Direction.Down))
        {
            return Direction.Down;
        }
        return Direction.Null;
    }
}
