using UnityEngine;
using UnityEditor;
using SpaceEngine;

[CustomEditor(typeof(Star))]
public class StarEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.Space(7f);
        
        Star star = target as Star;

        if (GUILayout.Button("Create"))
        {
            star.Create();
        }
        
        if (GUILayout.Button("Create random star"))
        {
            star.Class = SpaceEngine.StarClass.Random;
            star.Create();
        }

        if (GUILayout.Button("Evolve"))
        {
            star.Evolve();
        }

        GUILayout.Space(10f);
        if (GUILayout.Button("Add planet"))
        {
            star.AddPlanet();
        }

        if (GUILayout.Button("Delete planets"))
        {
            star.DestroyPlanets();
        }
    }

    private void OnSceneGUI()
    {
        Star star = target as Star;
        if (star.ShowEcoZone)
        {
            Handles.color = new Color32(173, 46, 39, 255);
            Handles.Disc(Quaternion.identity, star.transform.position, new Vector3(0, 1, 0), (star.EcoZone.From * SpaceMath.AU) / SpaceMath.Unit, false, 0f);
            Handles.color = new Color32(23, 241, 255, 255);
            Handles.Disc(Quaternion.identity, star.transform.position, new Vector3(0, 1, 0), (star.EcoZone.To * SpaceMath.AU) / SpaceMath.Unit, false, 0f);
        }
    }
}