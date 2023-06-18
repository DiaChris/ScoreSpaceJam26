using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelManager))]
public class LevelManagerEditor : Editor
{
    Vector2 scroll;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();

        LevelManager data = (LevelManager)target;

        if(GUILayout.Button("Add point"))
        {
            data.AddPoint();
        }


        scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.MaxHeight(300));
        

        for (int i = 0; i < 100; i++)
        {
            EditorGUILayout.BeginHorizontal("box");
            EditorGUILayout.LabelField("Difficulty level: " + (i));
            EditorGUILayout.LabelField(((int)data._DifficultyLevelCurve.Evaluate(i)) + " points required" );
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView();
    }
}
