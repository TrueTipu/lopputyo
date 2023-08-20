using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class VectorReference
{
    [SerializeField]
    Vector2 pos;
    public Vector2 Pos => pos;

    public void SetPos(Vector2 _pos)
    {
        pos = _pos;
    }

    [SerializeField]
    Vector2 middle;
    public Vector2 Middle => middle;

    public void SetMiddle(Vector2 _middle)
    {
        middle = _middle;
    }
}
namespace RoomStreamPathNodes
{

    [Serializable]
    internal class Node
    {


        [SerializeField]
        VectorReference vectorReference;

        internal VectorReference VectorReference => vectorReference;

        [SerializeField]
        List<VectorReference> linkedNodes;

        public Vector2 Pos => (vectorReference.Pos + vectorReference.Middle);

        [field: NonSerialized]
        public List<Node> LinkedNodes { get; private set; }

        public List<Vector2> GetConnectedPoints()
        {
            return LinkedNodes.ConvertAll(x => x.vectorReference.Pos + x.vectorReference.Middle);
        }

        public void SetLocalLinkData(PathNodes _creator)
        {
            LinkedNodes = linkedNodes.ConvertAll(x => _creator.FindWithPos(x.Pos + x.Middle)).FindAll(x => x != null);
        }

        public void Init(Vector2 _position, Vector2 _middle)
        {
            LinkedNodes = new List<Node>();
            linkedNodes = new List<VectorReference>();
            vectorReference = new VectorReference();
            vectorReference.SetPos(_position - _middle);
            vectorReference.SetMiddle(_middle);
        }

        public static Node CreateInstance(Vector2 _position, Vector2 _middle)
        {
            var data = new Node();
            data.Init(_position, _middle);
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
            if(LinkedNodes == null)
            {
                linkedNodes = new List<VectorReference>();
            }
            else
            {
                linkedNodes = LinkedNodes.ConvertAll(x => x.VectorReference);
            }
         
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
            vectorReference.SetPos(_pos - vectorReference.Middle);
        }
        public void ChangeMiddle(Vector2 _middle)
        {
            vectorReference.SetMiddle(_middle);
        }
        public override string ToString()
        {
            return "POS: " + vectorReference.Pos.ToString() + " LINK AMOUNT" + LinkedNodes?.Count;
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

