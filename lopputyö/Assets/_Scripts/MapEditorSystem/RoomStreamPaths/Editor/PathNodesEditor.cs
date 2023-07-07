using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(RoomObject))]
public class PathNodesEditor : Editor
{
    RoomObject pathNodeHandler;
    PathNodes PathNodes
    {
        get { return pathNodeHandler.PathNodes; }
    }

    PathNodes.Node selectedNode = null;

    const float SEDGMENT_SELECT_DIS = 0.1f;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUI.BeginChangeCheck();

        if (GUILayout.Button("Reset nodes"))
        {
            Undo.RecordObject(pathNodeHandler, "Reset"); //tallennus undo varten

            pathNodeHandler.CreateNodes();
        }

        if (EditorGUI.EndChangeCheck()) //jos muutoksia niin maalataan
        {
            SceneView.RepaintAll();
        }
    }

    private void OnSceneGUI()
    {
        Input();
        Draw();
    }

    void Input()
    {
        Event _guiEvent = Event.current;
        Vector2 _mousePos = HandleUtility.GUIPointToWorldRay(_guiEvent.mousePosition).origin;

        if (_guiEvent.type == EventType.MouseDown && _guiEvent.button == 0 && _guiEvent.shift)
        {
            selectedNode = null;

            {
                Undo.RecordObject(pathNodeHandler, "Add node");
                PathNodes.AddMiddlePoint(_mousePos);
            }
        }


        if (_guiEvent.type == EventType.MouseDown && _guiEvent.button == 0 && _guiEvent.control)
        {
            {

                int _closestMiddle =
                    Helpers.FindMin(pathNodeHandler.MiddlePointSize,//max matka klikkaukselle
                        (i) => { return Vector2.zero.Distance(_mousePos, PathNodes.GetMiddlePoint(i)); },
                        PathNodes.MiddlePointCount);
                int _closestBorder =
                    Helpers.FindMin(pathNodeHandler.BorderPointSize,//max matka klikkaukselle
                        (i) => { return Vector2.zero.Distance(_mousePos, PathNodes.GetBorderPoint(i)); },
                        PathNodes.BorderPointCount);


                if (_closestMiddle != -1)
                {
                    if (selectedNode != null)
                    {
                        Connect(selectedNode, PathNodes.GetMiddlePointNode(_closestMiddle));
                    }
                    else { selectedNode = PathNodes.GetMiddlePointNode(_closestMiddle); }
                }
                else if (_closestBorder != -1)
                {
                    if (selectedNode != null)
                    {
                        Connect(selectedNode, PathNodes.GetBorderPointNode(_closestBorder));
                    }
                    else { selectedNode = PathNodes.GetBorderPointNode(_closestBorder); }
                }
                Undo.RecordObject(pathNodeHandler, "Select Node");
            }
        }

        if (_guiEvent.type == EventType.MouseDown && _guiEvent.button == 1)
        {
            selectedNode = null;

            //selvitetään lähin
            int _closestMiddle =
            Helpers.FindMin(pathNodeHandler.MiddlePointSize, //max matka klikkaukselle
                (i) => { return Vector2.zero.Distance(_mousePos, PathNodes.GetMiddlePoint(i)); },
                PathNodes.MiddlePointCount);

            if (_closestMiddle != -1) //jos ei ole default, poista
            {
                Undo.RecordObject(pathNodeHandler, "Delete node");

                PathNodes.DeleteMiddlePoint(_closestMiddle);
            }
        }

        HandleUtility.AddDefaultControl(0);

        void Connect(PathNodes.Node _firstNode, PathNodes.Node _secondNode)
        {
            PathNodes.AddLink(_firstNode, _secondNode);
            selectedNode = _secondNode;
        }
    }



    void Draw()
    {

        var _doublePrevent = new Dictionary<Vector2, List<Vector2>>();
        Handles.color = Color.black;

        for (int i = 0; i < PathNodes.MiddlePointCount; i++)
        {
            List<Vector2> _targets = PathNodes.GetLinks(i);
            Vector2 _center = PathNodes.GetMiddlePoint(i);
            foreach (Vector2 _target in _targets)
            {
                if (!_doublePrevent.ContainsKey(_target))
                {
                    Handles.DrawLine(_center, _target, 2.5f);
                    _doublePrevent.Add(_target, new List<Vector2> { _center });
                }
                else if (!_doublePrevent[_target].Contains(_center))
                {
                    Handles.DrawLine(_center, _target, 2.5f);
                    _doublePrevent[_target].Add(_center);
                }
            }
        }


        //piirtää jokaista pathpistettä kohden liikutusjutun, aika itseselitteinen kun lukee vähän
        for (int i = 0; i < PathNodes.MiddlePointCount; i++)
        {
            Handles.color = (PathNodes.GetMiddlePointNode(i) == selectedNode) ? pathNodeHandler.SelectedPointColor : pathNodeHandler.MiddlePointColor;
            float _handleSize = pathNodeHandler.MiddlePointSize;

            Vector2 _newPos = Handles.FreeMoveHandle(PathNodes.GetMiddlePoint(i), Quaternion.identity, _handleSize, Vector2.zero, Handles.CylinderHandleCap);
            if (PathNodes.GetMiddlePoint(i) != _newPos)
            {
                Undo.RecordObject(pathNodeHandler, "Move points"); //tallennus undo varten
                selectedNode = null;
                PathNodes.MoveMiddlePoint(i, _newPos);
            }
        }
        for (int i = 0; i < PathNodes.BorderPointCount; i++)
        {
            Handles.color = (PathNodes.GetBorderPointNode(i) == selectedNode) ? pathNodeHandler.SelectedPointColor : pathNodeHandler.BorderPointColor;
            float _handleSize = pathNodeHandler.BorderPointSize;
            Handles.FreeMoveHandle(PathNodes.GetBorderPoint(i), Quaternion.identity, _handleSize, Vector2.zero, Handles.CylinderHandleCap);
        }
    }

    void OnEnable()
    {
        pathNodeHandler = (RoomObject)target; //target viittaa käytettäävään scriptiin
        if (pathNodeHandler.PathNodes == null)
        {
            pathNodeHandler.CreateNodes();
        }
        selectedNode = null;
    }
}
