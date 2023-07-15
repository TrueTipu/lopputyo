using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;
using System;

[CustomEditor(typeof(RoomObject))]
public class PathNodesEditor : Editor
{
    RoomObject pathNodeHandler;
    PathNodes PathNodes
    {
        get { return pathNodeHandler.PathNodes; }
    }

    const float SEDGMENT_SELECT_DIS = 0.1f;

    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUI.BeginChangeCheck();

        if (GUILayout.Button("Reset nodes"))
        {
            PrefabUtility.RecordPrefabInstancePropertyModifications(pathNodeHandler); //tallennus undo varten
            UnityEditor.Undo.RegisterFullObjectHierarchyUndo(pathNodeHandler, "reset");
            pathNodeHandler.CreateNodes();
            Undo.willFlushUndoRecord += PathNodes.ResetLocalDatas;
        }
        if (GUILayout.Button("Reset links"))
        {
            PathNodes.ResetLocalDatas();
        }
        bool _drawLines = GUILayout.Toggle(PathNodes.DrawLines, "DrawLineToggle");
        if (_drawLines != PathNodes.DrawLines)
        {
            PrefabUtility.RecordPrefabInstancePropertyModifications(pathNodeHandler); //tallennus undo varten
            UnityEditor.Undo.RegisterFullObjectHierarchyUndo(pathNodeHandler, "DrawLineToggle");
            PathNodes.DrawLines = _drawLines;
            Debug.Log("moi");
        }

        if (EditorGUI.EndChangeCheck()) //jos muutoksia niin maalataan
        {
            SceneView.RepaintAll();
        }
    }
    void DrawLines()
    {

        Handles.color = Color.black;

        for (int i = 0; i < PathNodes.MiddlePointCount; i++)
        {
            List<Vector2> _targets = PathNodes.GetLinks(i);

            if (_targets == null || _targets.Count == 0) continue;

            Vector2 _center = PathNodes.GetMiddlePoint(i);
            foreach (Vector2 _target in _targets)
            {
                Handles.DrawLine(_center, _target, 2.5f);

            }
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

            {
                UnityEditor.PrefabUtility.RecordPrefabInstancePropertyModifications(pathNodeHandler);
                UnityEditor.Undo.RegisterFullObjectHierarchyUndo(pathNodeHandler, "Spawn");
                PathNodes.AddMiddlePoint(_mousePos, pathNodeHandler);
                //PathNodes.MoveMiddlePoint(PathNodes.MiddlePointCount - 1, _mousePos);
            }
        }
        if (_guiEvent.type == EventType.MouseDown && _guiEvent.button == 0 && !_guiEvent.control)
        {
            PathNodes.RemoveSelect();
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

                UnityEditor.PrefabUtility.RecordPrefabInstancePropertyModifications(pathNodeHandler);
                UnityEditor.Undo.RegisterFullObjectHierarchyUndo(this, "Select");
                PathNodes.SelectNode(_closestMiddle, _closestBorder);

            }
        }

        if (_guiEvent.type == EventType.MouseDown && _guiEvent.button == 1)
        {

            //selvitetään lähin
            int _closestMiddle =
            Helpers.FindMin(pathNodeHandler.MiddlePointSize, //max matka klikkaukselle
                (i) => { return Vector2.zero.Distance(_mousePos, PathNodes.GetMiddlePoint(i)); },
                PathNodes.MiddlePointCount);

            if (_closestMiddle != -1) //jos ei ole default, poista
            {
                UnityEditor.PrefabUtility.RecordPrefabInstancePropertyModifications(pathNodeHandler);
                UnityEditor.Undo.RegisterFullObjectHierarchyUndo(pathNodeHandler, "Delete");

                PathNodes.DeleteMiddlePoint(_closestMiddle);
            }
        }

        HandleUtility.AddDefaultControl(0);


    }



    void Draw()
    {
        if (PathNodes.DrawLines) DrawLines();

        //piirtää jokaista pathpistettä kohden liikutusjutun, aika itseselitteinen kun lukee vähän
        for (int i = 0; i < PathNodes.MiddlePointCount; i++)
        {
            Handles.color = (PathNodes.IsSelectedMiddleNode(i)) ? pathNodeHandler.SelectedPointColor : pathNodeHandler.MiddlePointColor;
            float _handleSize = pathNodeHandler.MiddlePointSize;

            Vector2 _newPos = Handles.FreeMoveHandle(PathNodes.GetMiddlePoint(i), Quaternion.identity, _handleSize, Vector2.zero, Handles.CylinderHandleCap);
            if (PathNodes.GetMiddlePoint(i) != _newPos)
            {
                PrefabUtility.RecordPrefabInstancePropertyModifications(pathNodeHandler); //tallennus undo varten
                UnityEditor.Undo.RegisterFullObjectHierarchyUndo(pathNodeHandler, "move");
                PathNodes.MoveMiddlePoint(i, _newPos);
            }
        }
        for (int i = 0; i < PathNodes.BorderPointCount; i++)
        {
            Handles.color = (PathNodes.IsSelectedBorderNode(i)) ? pathNodeHandler.SelectedPointColor : pathNodeHandler.BorderPointColor;
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
        PathNodes.ResetLocalDatas();
        Undo.undoRedoPerformed += () => PathNodes.ResetLocalDatas();
    }
}
