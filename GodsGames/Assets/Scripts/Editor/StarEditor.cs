using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Star))]
public class StarEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.Space(7f);
        
        Star star = target as Star;

        if(GUILayout.Button("Create"))
        {
            star.CreateStar();
        }

        if (GUILayout.Button("Create random star"))
        {
            star.Class = SpaceEngine.StarClass.Random;
            star.CreateStar();
        }

        if (GUILayout.Button("Evolve"))
        {
            star.EvolveStar();
        }
    }
}
