using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Threading.Tasks;
using System.Collections;

public class PathNodes
{
    const int BORDER_POINTS_SIZE = 12;
    [SerializeField, HideInInspector]
    List<Node> middlePoints;
    public Node GetMiddlePointNode(int _index) => middlePoints[_index];
    public Vector2 GetMiddlePoint(int _index) => middlePoints[_index].Pos;

    [SerializeField, HideInInspector]
    Node[] borderPoints;
    public Vector2 GetBorderPoint(int _index) => borderPoints[_index].Pos;
    public Node GetBorderPointNode(int _index) => borderPoints[_index];


    public PathNodes(Vector2 _centre, Vector2 _middleDis, Vector2 _borderDis)
    {
        middlePoints = ToNodeList(new List<Vector2>
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
        });

        borderPoints = ToNodeList(new List<Vector2>
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
        }).ToArray();
    }

    //void WideSearch(int enterPoint, int exitPoint)
    //{
    //    List<Node> _result = new List<Node>();
    //    Queue<Node> _queue = new Queue<Node>();
    //    Node _current = middlePoints[enterPoint];

    //    _queue.Enqueue(_current);

    //    while (_queue.Count > 0)
    //    {
    //        _current = _queue.Dequeue();
    //        _result.Add(_current);
    //        _queue..Append(_current.GetConnectedPoints());
    //    }
    //    return _result;

    //}



    public bool HasConnection(int point1, int point2)
    {
        throw new NotImplementedException();
    }

    public int MiddlePointCount => middlePoints.Count;
    public int BorderPointCount => BORDER_POINTS_SIZE;

    public void AddMiddlePoint(Vector2 _anchorPos)
    {
        middlePoints.Add(new Node(_anchorPos));
    }


    public void DeleteMiddlePoint(int _index)
    {
        if (MiddlePointCount <= 1) return;

        CutLinks(_index);

        middlePoints.RemoveAt(_index);
    }

    public List<Vector2> GetLinks(int _index)
    {
        return middlePoints[_index].GetConnectedPoints();
    }
    public void CutLinks(int _index)
    {
        middlePoints[_index].ResetLinks();
    }
    public void AddLink(Node _nodeOne, Node _nodeTwo)
    {
        _nodeOne.AddLinkedNode(_nodeTwo);
    }

    public void MoveMiddlePoint(int _index, Vector2 _pos)
    {
        Vector2 _deltaMove = _pos - middlePoints[_index].Pos;

        middlePoints[_index].ChangePos(_pos);

    }

    [System.Serializable]
    public class Node
    {
        
        Vector2 position;
        public Vector2 Pos => position;
        HashSet<Node> linkedNodes = new HashSet<Node>();
        public void ResetLinks()
        {
            foreach (Node node in linkedNodes)
            {
                node.linkedNodes.Remove(this);
            }
        }

        public List<Vector2> GetConnectedPoints()
        {
            return linkedNodes.ToList().ConvertAll((n) => n.Pos);
        }

        public Node(Vector2 _position)
        {
            position = _position;
        }

        public void AddLinkedNode(Node _node)
        {
            linkedNodes.Add(_node);
            _node.linkedNodes.Add(this);
        }
        public void RemoveNode(Node _node)
        {
            linkedNodes.Remove(_node);
        }

        public void ChangePos(Vector2 _pos)
        {
            position = _pos;
        }
    }

    List<Node> ToNodeList(List<Vector2> _list)
    {
        List<Node> _nodeList = new List<Node>();
        foreach (Vector2 _pos in _list)
        {
            _nodeList.Add(new Node(_pos));
        }

        return _nodeList;
    }
}

