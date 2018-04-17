using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StarSystem))]
public class StarSystemEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.Space(7f);

        StarSystem starSystem = target as StarSystem;

        if (GUILayout.Button("Create"))
        {
            if (starSystem.transform.childCount != 0) starSystem.DestroyStarSystem();
            starSystem.Create();
        }
    }

}
