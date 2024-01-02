using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
static class DirectionExtension
{
    public static Direction ReturnAntiDir(this Direction _dir)
    {
        switch (_dir)
        {
            case Direction.Null:
                return Direction.Null;
            case Direction.Left:
                return Direction.Right;
            case Direction.LeftUp:
                return Direction.RightUp;
            case Direction.LeftDown:
                return Direction.RightDown;
            case Direction.Right:
                return Direction.Left;
            case Direction.RightUp:
                return Direction.LeftUp;
            case Direction.RightDown:
                return Direction.LeftDown;
            case Direction.Up:
                return Direction.Down;
            case Direction.UpLeft:
                return Direction.DownLeft;
            case Direction.UpRight:
                return Direction.DownRight;
            case Direction.Down:
                return Direction.Up;
            case Direction.DownLeft:
                return Direction.UpLeft;
            case Direction.DownRight:
                return Direction.UpRight;
            default:
                return Direction.Null;
        }
    }

    public static Direction DoorIntToDir(int _doorI)
    {
        switch (_doorI)
        {
            case 0:
                return Direction.LeftDown;
            case 1:
                return Direction.Left;
            case 2:
                return Direction.LeftUp;
            case 3:
                return Direction.UpLeft; 
            case 4:
                return Direction.Up;
            case 5:
                return Direction.UpRight;  
            case 6:
                return Direction.RightUp;
            case 7:
                return Direction.Right;
            case 8:
                return Direction.RightDown;
            case 9:
                return Direction.DownRight;
            case 10:
                return Direction.Down;
            case 11:
                return Direction.DownLeft;
            default:
                return Direction.None;
        }
    }
}
public enum Direction
{
    Null,
    Left,
    LeftUp,
    LeftDown,
    Right,
    RightUp,
    RightDown,
    Up,
    UpLeft,
    UpRight,
    Down,
    DownLeft,
    DownRight,
    None
}


