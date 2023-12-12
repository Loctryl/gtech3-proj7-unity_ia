using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(AbstractDungeonGenerator), true)]
public class RandomDungonGeneratorEditor : Editor
{
    AbstractDungeonGenerator generator;

    private void Awake()
    {
        generator = (AbstractDungeonGenerator)target;
    }

    override public void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Generate"))
        {
            generator.GenerateDungeon();
        }
    }
}
