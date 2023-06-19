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
        return (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z));
    }
    public static bool JumpKeys()
    {
        return (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Z));
    }
    public static bool JumpKeysUp()
    {
        return (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Z));
    }
    #endregion

    #region dash
    public static bool DashKeysDown()
    {
        return (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.LeftShift));
    }
    public static bool DashKeys()
    {
        return (Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.LeftShift));
    }
    public static bool DashKeysUp()
    {
        return (Input.GetKeyUp(KeyCode.C) || Input.GetKeyUp(KeyCode.LeftShift));
    }
    #endregion

    #region Interact
    public static bool InteractKeysDown()
    {
        return (Input.GetKeyDown(KeyCode.I));
    }
    public static bool InteractKeys()
    {
        return (Input.GetKey(KeyCode.I));
    }
    public static bool InteractKeysUp()
    {
        return (Input.GetKeyUp(KeyCode.I));
    }
    #endregion
}