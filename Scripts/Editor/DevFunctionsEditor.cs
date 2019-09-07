using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DevFunctions))]
public class DevFunctionsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DevFunctions df = target as DevFunctions;

        if (GUILayout.Button("Calculate big object"))
        {
            df.CalculateBigObjects();
            // df.AddForce();
        }
    }

}