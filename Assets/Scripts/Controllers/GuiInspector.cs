using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(DataController))]
public class GuiInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DataController data = (DataController)target;

        if (GUILayout.Button("DeleteAll"))
        {
            data.RemoveAllData();
        }
    }
}
