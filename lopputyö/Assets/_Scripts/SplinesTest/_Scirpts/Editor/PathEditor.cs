using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(PathCreator))]
public class PathEditor : Editor
{
    PathCreator creator;
    Path Path
    {
        get { return creator.Path; }
    }

    const float SEDGMENT_SELECT_DIS = 0.1f;
    int selectedSegmentIndex = -1;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUI.BeginChangeCheck();

        if(GUILayout.Button("Create new"))
        {
            Undo.RecordObject(creator, "Reset"); //tallennus undo varten

            creator.CreatePath();       
        }

        bool _autoSetControlPoints = GUILayout.Toggle(Path.AutoSetControlPoints, "Auto Set Control Points");
        if(_autoSetControlPoints != Path.AutoSetControlPoints)
        {
            Undo.RecordObject(creator, "Toddle auto set controls");
            Path.SetAutoSetMode(_autoSetControlPoints);
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
        Event _guiEvent = Event.current; //toinen tapa inputtiin, luultavasti editor scriptei varten, en jaksa research atm, kyl sen tajuaa
        Vector2 _mousePos = HandleUtility.GUIPointToWorldRay(_guiEvent.mousePosition).origin;

        if(_guiEvent.type == EventType.MouseDown && _guiEvent.button == 0 && _guiEvent.shift)
        {
            if (selectedSegmentIndex != -1) //jos löydetty keskellä oleva indexi
            {
                Undo.RecordObject(creator, "Split segment");
                Path.SplitSegment(_mousePos, selectedSegmentIndex);
            }
            else
            {
                Undo.RecordObject(creator, "Add segment");
                Path.AddSegment(_mousePos);
            }           
        }

        if (_guiEvent.type == EventType.MouseDown && _guiEvent.button == 1)
        {
            //selvitetään lähin
            int _closestAnchor = 
            Helpers.FindMin(creator.AnchorDiameter * 1.5f,//max matka klikkaukselle
                (i) => { return Vector2.zero.Distance(_mousePos, Path[i]); },
                Path.NumPoints, 3);

            if(_closestAnchor != -1) //jos ei ole default, poista
            {
                Undo.RecordObject(creator, "Delete segment");
                Path.DeleteSegment(_closestAnchor);
            }
        }

        //inserttiä varten lähimmän välin laskeminen
        //tallennetaan lähimmäksi epä -1, jos löytyy välipiste
        if (_guiEvent.type == EventType.MouseMove)
        {
            int _newSelection = 
            Helpers.FindMin(SEDGMENT_SELECT_DIS,
                (i) => { var _p = Path.GetPointsInSegment(i); return HandleUtility.DistancePointBezier(_mousePos, _p[0], _p[3], _p[1], _p[2]); },
                Path.NumSegments);
        

            if(_newSelection != selectedSegmentIndex)
            {
                selectedSegmentIndex = _newSelection;
                HandleUtility.Repaint();
            }
        }

        HandleUtility.AddDefaultControl(0);
    }

    void Draw()
    {
        for (int i = 0; i < Path.NumSegments; i++)
        {
            Vector2[] _points = Path.GetPointsInSegment(i);
            Handles.color = Color.black;
            if (creator.DisplayControlPoints)
            {
                Handles.DrawLine(_points[1], _points[0]);
                Handles.DrawLine(_points[2], _points[3]);
            }
            
            Color segmentCol = (i == selectedSegmentIndex && Event.current.shift) ? creator.SelectedSegmentCol : creator.SegmnetCol;
            Handles.DrawBezier(_points[0], _points[3], _points[1], _points[2], segmentCol, null, 2); //beizer piirto valmiina nyt alkuun, tehdään myöhemmin itse
        }


        //piirtää jokaista pathpistettä kohden liikutusjutun, aika itseselitteinen kun lukee vähän
        for (int i = 0; i < Path.NumPoints; i++)
        {
            if (i % 3 != 0 && !creator.DisplayControlPoints) continue; //jatka jos ei haluta controlpointteja, eikä ole sellainen

            Handles.color = (i % 3 == 0) ? creator.AnchorColor : creator.ControlCol;
            float _handleSize = (i % 3 == 0) ? creator.AnchorDiameter : creator.ComtrolDiameter;

            Vector2 _newPos = Handles.FreeMoveHandle(Path[i], Quaternion.identity, _handleSize, Vector2.zero, Handles.CylinderHandleCap);
            if(Path[i] != _newPos)
            {
                Undo.RecordObject(creator, "Move points"); //tallennus undo varten
                Path[i] = _newPos;
            }
        }
    }

    void OnEnable()
    {
        creator = (PathCreator)target; //target viittaa käytettäävään scriptiin
        if(creator.Path == null)
        {
            creator.CreatePath();
        }
    }
}
