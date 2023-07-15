using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Threading.Tasks;
using System.Collections;

[System.Serializable]
public class PathNodes
{
    const int BORDER_POINTS_SIZE = 12;
    [SerializeField, HideInInspector]
    List<Node> middlePoints;
    public Vector2 GetMiddlePoint(int _index) => middlePoints[_index].Pos;
    private Node MiddlePointNode(int _index) => middlePoints[_index];

    public bool DrawLines { get; set; } = false;

   

    public Node FindWithPos(Vector2 x)
    {
        var _node = middlePoints.Find((a) => a.Pos.Equals(x));
        return (_node != null) ? _node : borderPoints.ToList().Find((a) => a.Pos.Equals(x));
    }



    Node selectedNode = null;

    [SerializeField, HideInInspector]
    Node[] borderPoints;
    public Vector2 GetBorderPoint(int _index) => borderPoints[_index].Pos;
    private Node BorderPointNode(int _index) => borderPoints[_index];



    public PathNodes(Vector2 _centre, Vector2 _middleDis, Vector2 _borderDis)
    {

        middlePoints = (new List<Vector2>
        {
            _centre,
            _centre + Vector2.left * _middleDis.x,
            _centre + (Vector2.left + Vector2.up) * _middleDis,
            _centre + Vector2.up * _middleDis.y,
            _centre + (Vector2.up + Vector2.right) * _middleDis,
            _centre + Vector2.right * _middleDis.x,
            _centre + (Vector2.right + Vector2.down) * _middleDis,
             _centre + Vector2.down * _middleDis.y,
             _centre + (Vector2.down + Vector2.left) * _middleDis,
        }).ConvertAll((v) => Node.CreateInstance(v, this));

        borderPoints = (new List<Vector2>
        {
            middlePoints[8].Pos + Vector2.left * _borderDis.x,
            middlePoints[1].Pos + Vector2.left * _borderDis.x,
            middlePoints[2].Pos + Vector2.left * _borderDis.x,
            middlePoints[2].Pos + Vector2.up * _borderDis.y,
            middlePoints[3].Pos + Vector2.up * _borderDis.y,
            middlePoints[4].Pos + Vector2.up * _borderDis.y,
            middlePoints[4].Pos + Vector2.right * _borderDis.x,
            middlePoints[5].Pos + Vector2.right * _borderDis.x,
            middlePoints[6].Pos + Vector2.right * _borderDis.x,
            middlePoints[6].Pos + Vector2.down * _borderDis.y,
            middlePoints[7].Pos + Vector2.down * _borderDis.y,
            middlePoints[8].Pos + Vector2.down * _borderDis.y,
        }).ConvertAll((v) => Node.CreateInstance(v, this)).ToArray();

        //linkNodes = new HashSet<KeyValuePair<Vector2, Vector2>>();
        selectedNode = null;
    }


    public async void ResetLocalDatas()
    {
        await Task.Delay(10);
        Debug.Log("Moi");
        foreach (Node _node in middlePoints)
        {
            _node.SetCreator(this);
        }
        foreach (Node _node in borderPoints)
        {
            _node.SetCreator(this);
        }
    }
    public void RemoveSelect()
    {
        selectedNode = null;
    }

    List<Node> LongSearch(Node _enterPoint, Node _exitPoint)
    {
        var _map = new Dictionary<Node, Tuple<int, Node>>();
        Stack<Node> _stack = new Stack<Node>();
        Node _current = _enterPoint;

        _stack.Push(_current);
        int _layer = 0;
        _map[_current] = new Tuple<int, Node>(0, null);
        while (_stack.Count > 0)
        {
            _current = _stack.Pop();
            _layer = _map[_current].Item1;

            foreach (Node _node in _current.LinkedNodes)
            {

                if (_map.ContainsKey(_node) && (_map[_node].Item1 < _layer + 1))
                {
                     continue;
                }
                else
                {
                    _map[_node] = new Tuple<int, Node>(_layer + 1, _current);
                    _stack.Push(_node);
                }
            }
        }

        if (_map.ContainsKey(_exitPoint))
        {
            List<Node> _result = new List<Node>();
            Node _backCurrent = _exitPoint;
            _result.Add(_exitPoint);
            while (_backCurrent != _enterPoint)
            {
                _backCurrent = _map[_backCurrent].Item2;
                _result.Add(_backCurrent);
            }

            if (_result != null) _result.ForEach(n => Debug.Log(n));

            return _result;
        }
        else return null;

    }
    public bool IsSelectedMiddleNode(int _index)
    {
        return middlePoints[_index] == selectedNode;
    }
    public bool IsSelectedBorderNode(int _index)
    {
        return borderPoints[_index] == selectedNode;
    }

    public void SelectNode(int _closestMiddle, int _closestBorder)
    {
        if (_closestMiddle != -1)
        {
            if (selectedNode != null)
            {
                //Debug.Log(selectedNode);
                Connect(selectedNode, MiddlePointNode(_closestMiddle));
            }
            else { selectedNode = MiddlePointNode(_closestMiddle);}
        }
        else if (_closestBorder != -1)
        {
            if (selectedNode != null)
            {
                Connect(selectedNode, BorderPointNode(_closestBorder));
            }
            else {  selectedNode = BorderPointNode(_closestBorder);  }
        }


        void Connect(Node _firstNode, Node _secondNode)
        {
            if (_firstNode.GetConnectedPoints().Contains(_secondNode.Pos))
            {
                _firstNode.RemoveNode(_secondNode);
                selectedNode = null;
            }
            else
            {
                _firstNode.AddLinkedNode(_secondNode);
                selectedNode = _secondNode;
            }
        }
    }

    public bool HasConnection(int point1, int point2)
    {
        List<Node> _path = LongSearch(borderPoints[point1], borderPoints[point2]);
        return _path != null;
    }

    public int MiddlePointCount => middlePoints.Count;
    public int BorderPointCount => BORDER_POINTS_SIZE;

    public void AddMiddlePoint(Vector2 _anchorPos, RoomObject _pathNodeHandler)
    {
        selectedNode = null;
        middlePoints.Add(Node.CreateInstance(_anchorPos, this));
        middlePoints[middlePoints.Count - 1].ChangePos(_anchorPos);
    }


    public void DeleteMiddlePoint(int _index)
    {
        selectedNode = null;

        if (MiddlePointCount <= 1) return;

        CutLinks(_index);
        var a = middlePoints.ElementAt(_index);
        middlePoints.Remove(a);
    }

    public List<Vector2> GetLinks(int _index)
    {
        try
        {
            return middlePoints[_index].GetConnectedPoints();
        }
        catch (Exception e)
        {
            return null;
        }
      
    }
    public void CutLinks(int _index)
    {
        middlePoints[_index].ResetLinks();
    }


    public void MoveMiddlePoint(int _index, Vector2 _pos)
    {
        selectedNode = null;

        Vector2 _deltaMove = _pos - middlePoints[_index].Pos;

        middlePoints[_index].ChangePos(_pos);

    }

}

