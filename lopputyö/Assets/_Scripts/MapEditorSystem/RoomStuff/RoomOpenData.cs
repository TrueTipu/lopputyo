using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


//hienoa editoria varten piti kehittää turhan complexi koodi, tekee siis vain ja ainoastaan sen bool array grid jutun inspectoriin
[System.Serializable]
public class RoomOpenData
{

    const int SIZE = 5;

    public RoomOpenData()
    {
        cells = new Row[SIZE]
        {
            new Row(new List<SerializableDirectionBoolPair>()
            {
                null, GetPair(Direction.UpLeft),GetPair(Direction.Up),GetPair(Direction.UpRight),null
            }),
            new Row(new List<SerializableDirectionBoolPair>()
            {
                GetPair(Direction.LeftUp), null,null,null,GetPair(Direction.RightUp)
            }),
            new Row(new List<SerializableDirectionBoolPair>()
            {
                GetPair(Direction.Left), null,null,null,GetPair(Direction.Right)
            }),
            new Row(new List<SerializableDirectionBoolPair>()
            {
                GetPair(Direction.LeftDown), null,null,null,GetPair(Direction.RightDown)
            }),
            new Row(new List<SerializableDirectionBoolPair>()
            {
                null, GetPair(Direction.DownLeft),GetPair(Direction.Down),GetPair(Direction.DownRight),null
            }),
       };
    }

    public bool this[Direction d]
    {
        get => GetPair(d).IsOpen;
    }

    

    [SerializeField]
    [HideInInspector]
    private Vector2Int gridSize = Vector2Int.one * SIZE;

    [SerializeField]
    [HideInInspector]
    private Vector2Int cellSize;

    [System.Serializable]
    class Row
    {
        [SerializeField]
        private SerializableDirectionBoolPair[] row = new SerializableDirectionBoolPair[SIZE];

        public Row(List<SerializableDirectionBoolPair> _list)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                if (_list[i] == null)
                {
                    this[i] = new SerializableDirectionBoolPair(Direction.Null, false);
                }
                this[i] = _list[i];
            }
        }

        public SerializableDirectionBoolPair this[int i]
        {
            get => row[i];
            set => row[i] = value;
        }
    }

    [SerializeField]
    private Row[] cells;

    [System.Serializable]
    class SerializableDirectionBoolPair
    {
        public SerializableDirectionBoolPair(Direction _dir, bool _value)
        {
            dir = _dir;
            isOpen = _value;
        }
        [SerializeField]
        private Direction dir;
        public Direction Dir => dir;

        [SerializeField]
        private bool isOpen;
        public bool IsOpen => isOpen;
    }

    List<SerializableDirectionBoolPair>  directionPairs = new List<SerializableDirectionBoolPair>()
    {
        new SerializableDirectionBoolPair(Direction.Up, false),
        new SerializableDirectionBoolPair(Direction.Down, false),
        new SerializableDirectionBoolPair(Direction.Left, false),
        new SerializableDirectionBoolPair(Direction.Right, false),
        new SerializableDirectionBoolPair(Direction.UpLeft, false),
        new SerializableDirectionBoolPair(Direction.DownLeft, false),
        new SerializableDirectionBoolPair(Direction.LeftUp, false),
        new SerializableDirectionBoolPair(Direction.RightUp, false),
        new SerializableDirectionBoolPair(Direction.UpRight, false),
        new SerializableDirectionBoolPair(Direction.DownRight, false),
        new SerializableDirectionBoolPair(Direction.LeftDown, false),
        new SerializableDirectionBoolPair(Direction.RightDown, false),
    };

    SerializableDirectionBoolPair GetPair(Direction _dir)
    {
        return directionPairs.First(d => d.Dir == _dir);
    }


}
