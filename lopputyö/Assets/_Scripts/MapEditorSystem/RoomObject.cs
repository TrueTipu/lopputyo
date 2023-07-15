using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//yksinkertainen koodi prefabille ja sille spesifeille toiminnoille, ks Room.cs
public class RoomObject : MonoBehaviour
{
    [SerializeField, HideInInspector]
    public PathNodes PathNodes;



    [field: SerializeField] public Vector2 MiddleDis { get; private set; } = Vector2.one;
    [field: SerializeField] public Vector2 BorderDis { get; private set; } = Vector2.one;

    [field: SerializeField] public float MiddlePointSize { get; private set; } = 5f;
    [field: SerializeField] public float BorderPointSize { get; private set; } = 5f;

    [field: SerializeField] public Color MiddlePointColor { get; private set; } = Color.cyan;


    [field: SerializeField] public Color BorderPointColor { get; private set; } = Color.red;
    [field: SerializeField] public Color SelectedPointColor { get; private set; } = Color.green;

    List<int >enterPoint = new List<int>();
    List<int> exitPoint = new List<int>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(PathNodes.HasConnection(0, 3));
            Debug.Log("");
            Debug.Log("");
            Debug.Log(PathNodes.HasConnection(1, 3));
            Debug.Log("");
            Debug.Log("");
            Debug.Log(PathNodes.HasConnection(5, 3));
            Debug.Log("");
            Debug.Log("");
        }

    }

    public void CreateNodes()
    {
        PathNodes = new PathNodes(transform.position, MiddleDis, BorderDis);
    }

    int GetClosest(Vector2 _pos)
    {
        return Helpers.FindMin(Mathf.Infinity,
            (i) => { return Vector2.zero.Distance(_pos, PathNodes.GetBorderPoint(i)); },
            PathNodes.BorderPointCount);
    }
    public bool IsInPath(Vector2 _playerPos)
    {
        int _closestBorder = GetClosest(_playerPos);

        if (_closestBorder != -1)
        {
            return PathNodes.HasConnection(enterPoint[enterPoint.Count-1], _closestBorder);

        }
        else return false;
    }

    public void SetEnterPoint(Vector2 _playerPos, bool _needClear = false)
    {
        int _closestBorder = GetClosest(_playerPos);

        if(_needClear) { enterPoint = new List<int>(); }
        if (_closestBorder != -1)
        {
            enterPoint.Add(_closestBorder);
        }
    }
    public void RemoveLatestEnter()
    {
        if(enterPoint.Count > 0)
        enterPoint.RemoveAt(enterPoint.Count - 1);
    }

    public void SetExitPoint(Vector2 _playerPos, bool _needClear = false)
    {
        int _closestBorder = GetClosest(_playerPos);

        if (_needClear) { exitPoint = new List<int>(); }
        if (_closestBorder != -1)
        {
            exitPoint.Add(_closestBorder);
        }
    }
    public void RemoveLatestExit()
    {
        if (enterPoint.Count > 0)
            enterPoint.RemoveAt(enterPoint.Count - 1);
    }
    private void Reset() //automaattinen unity kutsu resetistä
    {
        CreateNodes();
    }

    public void Clear()
    {
        //if (leftBlock != null)
        //    leftBlock?.SetActive(false);
        //if (rightBlock != null)
        //    rightBlock?.SetActive(false);
        //if (upBlock != null)
        //    upBlock?.SetActive(false);
        //if(downBlock != null)
        //    downBlock?.SetActive(false);
    }

    public void BlockPath(Direction _dir)
    {
        //rewrite with new dirs
        switch (_dir)
        {
            case Direction.Left:

                break;
            case Direction.Right:

                break;
            case Direction.Up:

                break;
            case Direction.Down:

                break;
            default:
                break;
        }
    }

    public void SetActive(bool _active)
    {
        if (_active)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
