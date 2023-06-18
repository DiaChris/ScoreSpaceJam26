using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelManager))]
public class LevelManagerEditor : Editor
{
    Vector2 scroll;
    Vector2 scroll2;
    Vector2 scroll3;
    Vector2 scroll4;
    Vector2 scroll5;


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();

        LevelManager data = (LevelManager)target;

        if(GUILayout.Button("Add point"))
        {
            data.AddPoint();
        }

        if(GUILayout.Button("Final countdown"))
        {
            data.FinalLevel();
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

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Dmg zone spawn delay.");
        scroll2 = EditorGUILayout.BeginScrollView(scroll2, GUILayout.MaxHeight(300));
        for (int i = 0; i < 100; i++)
        {
            EditorGUILayout.BeginHorizontal("box");
            EditorGUILayout.LabelField("On level: " + (i));
            EditorGUILayout.LabelField((data._DamageZoneSpawnFrequencyCurve.Evaluate(i)) + " seconds delay." );
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndScrollView();

        EditorGUILayout.Space();
        
        EditorGUILayout.LabelField("Dmg zone speed.");
        scroll3 = EditorGUILayout.BeginScrollView(scroll3, GUILayout.MaxHeight(300));
        for (int i = 0; i < 100; i++)
        {
            EditorGUILayout.BeginHorizontal("box");
            EditorGUILayout.LabelField("On level: " + (i));
            EditorGUILayout.LabelField((data._DamageZoneMoveSpeedCurve.Evaluate(i)) + " value" );
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndScrollView();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Ground change delay.");
        scroll4 = EditorGUILayout.BeginScrollView(scroll4, GUILayout.MaxHeight(300));
        for (int i = 0; i < 100; i++)
        {
            EditorGUILayout.BeginHorizontal("box");
            EditorGUILayout.LabelField("On level: " + (i));
            EditorGUILayout.LabelField((data._GroundChangingCurve.Evaluate(i)) + " value" );
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndScrollView();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Number of instruments.");
        scroll5 = EditorGUILayout.BeginScrollView(scroll5, GUILayout.MaxHeight(300));
        for (int i = 0; i < 100; i++)
        {
            EditorGUILayout.BeginHorizontal("box");
            EditorGUILayout.LabelField("On level: " + (i));
            EditorGUILayout.LabelField((int)(data._MusicInstrumentsCurve.Evaluate(i)) + " value" );
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndScrollView();
        
    }
}
