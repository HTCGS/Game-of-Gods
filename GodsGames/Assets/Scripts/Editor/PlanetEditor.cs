using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Planet))]
public class PlanetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.Space(7f);

        Planet planet = target as Planet;

        if (GUILayout.Button("Create"))
        {
            planet.DestroySatellites();
            planet.Create();
        }

        if (GUILayout.Button("Create random planet"))
        {
            planet.Class = SpaceEngine.PlanetClass.Random;
            planet.DestroySatellites();
            planet.Create();
        }

        if (GUILayout.Button("Add satellite"))
        {
            planet.AddSatellite();
        }

        GUILayout.Space(10f);
        if (GUILayout.Button("Add satellite"))
        {
            planet.AddSatellite();
        }

        if (GUILayout.Button("Delete satellites"))
        {
            planet.DestroySatellites();
        }
    }
}
