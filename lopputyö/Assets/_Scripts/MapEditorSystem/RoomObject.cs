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


    [field: SerializeField] public StreamCreator RoomStream { get; private set; }

    [SerializeField] DirectionGameObjectDict blockObjectDict = new DirectionGameObjectDict();

    private void Start()
    {
        PathNodes.ResetLocalDatas();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log(PathNodes.HasConnection(0, 3));
            //Debug.Log("");
            //Debug.Log("");
            //Debug.Log(PathNodes.HasConnection(1, 3));
            //Debug.Log("");
            //Debug.Log("");
            //Debug.Log(PathNodes.HasConnection(5, 3));
            //Debug.Log("");
            //Debug.Log("");
        }

    }

    public void CreateNodes()
    {
        PathNodes = new PathNodes(transform.position, MiddleDis, BorderDis);
    }

    public int GetClosest(Vector2 _pos)
    {
        return Helpers.FindMin(Mathf.Infinity,
            (i) => { return Vector2.zero.Distance(_pos, PathNodes.GetBorderPoint(i)); },
            PathNodes.BorderPointCount);
    }
    public bool IsInPath(int _enterPoint, Vector2 _playerPos)
    {
        int _closestBorder = GetClosest(_playerPos);

        if (_closestBorder != -1)
        {
            return PathNodes.HasConnection(_enterPoint, _closestBorder);

        }
        else return false;
    }

    private void Reset() //automaattinen unity kutsu resetistä
    {
        CreateNodes();
    }

    public void Clear()
    {
        foreach(GameObject _object in blockObjectDict.Values)
        {
            _object.SetActive(false);
        }
    }

    public void BlockPath(Direction _dir)
    {
        blockObjectDict[_dir]?.SetActive(true);
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
