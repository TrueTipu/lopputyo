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


