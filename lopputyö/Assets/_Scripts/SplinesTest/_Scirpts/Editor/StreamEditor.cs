using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StreamPlacer))]
public class StreamEditor : Editor
{
    StreamPlacer creator;

    void OnSceneGUI()
    {
        if (creator.AutoUpdate && Event.current.type == EventType.Repaint) //ei suositeltu
        {
            creator.UpdateStream();
        }
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Redraw Particles"))
        {
            Undo.RecordObject(creator, "Redraw Particles"); //tallennus undo varten

            creator.UpdateParticles();
            SceneView.RepaintAll();
        }
    }
    void OnEnable()
    {
        creator = (StreamPlacer)target;
    }
}