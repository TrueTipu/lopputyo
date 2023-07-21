using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoomStreamPathNodes
{
    [Serializable]
    internal class Node
    {

        [NonSerialized]
        PathNodes creator;

        [SerializeField, HideInInspector]
        Vector2 position;
        public Vector2 Pos => position;

        [SerializeField, HideInInspector]
        List<Vector2> linkedNodes;

        [field: NonSerialized]
        public List<Node> LinkedNodes { get; private set; }

        public List<Vector2> GetConnectedPoints()
        {
            return LinkedNodes.ConvertAll(x => x.Pos);
        }

        public void SetCreator(PathNodes _creator)
        {
            creator = _creator;
            LinkedNodes = linkedNodes.ConvertAll(x => _creator.FindWithPos(x)).FindAll(x => x != null);
        }

        public void Init(Vector2 _position, PathNodes _creator)
        {
            creator = _creator;
            LinkedNodes = new List<Node>(); ;
            linkedNodes = new List<Vector2>();
            position = _position;
        }

        public static Node CreateInstance(Vector2 _position, PathNodes _creator)
        {
            var data = new Node();
            data.Init(_position, _creator);
            return data;
        }

        public void AddLinkedNode(Node _node)
        {
            LinkedNodes.Add(_node);
            _node.LinkedNodes.Add(this);
            _node.ResetLinkData();
            ResetLinkData();
        }
        public void RemoveNode(Node _node)
        {
            LinkedNodes.Remove(_node);
            _node.LinkedNodes.Remove(this);
            _node.ResetLinkData();
            ResetLinkData();
        }
        public void ResetLinkData()
        {
            linkedNodes = LinkedNodes.ConvertAll(x => x.Pos);
        }
        public void ResetLinks()
        {
            while (LinkedNodes.Count > 0)
            {
                RemoveNode(LinkedNodes[0]);
            }
        }

        public void ChangePos(Vector2 _pos)
        {
            position = _pos;
        }

        public override string ToString()
        {
            return "POS: " + Pos.ToString() + " LINK AMOUNT" + LinkedNodes.Count;
        }
    }


}



//static List<Node> ToNodeList(List<Vector2> _list)
//{
//    List<Node> _nodeList = new List<Node>();
//    foreach (Vector2 _pos in _list)
//    {
//        _nodeList.Add(new Node(_pos), this);
//    }

//    return _nodeList;
//}

