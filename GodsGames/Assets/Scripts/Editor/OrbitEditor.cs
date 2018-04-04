using System;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Orbit)), CanEditMultipleObjects]
public class OrbitEditor : Editor
{
    private SerializedProperty EccentricityRange;

    private float lastScale;

    private void OnEnable()
    {
        EccentricityRange = serializedObject.FindProperty("EccentricityRange");
        lastScale = 0;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //Orbit orbit = target as Orbit;
        EditorGUILayout.BeginHorizontal();
        //EditorGUILayout.LabelField("Eccentricity range");
        //EditorGUILayout.FloatField("From", 0);
        //EditorGUILayout.FloatField("To", 1);
        EditorGUILayout.EndHorizontal();

    }

    private void OnSceneGUI()
    {
        Orbit orbit = target as Orbit;

        float focus = orbit.Eccentricity;
        Vector3 toParent = orbit.Parent.transform.position - orbit.transform.position;
        float a = toParent.magnitude / (1 - focus);
        float b = Mathf.Sqrt(1 - Mathf.Pow(orbit.Eccentricity, 2)) * a;

        Vector3 center = orbit.transform.position + (toParent.normalized * a);
        Vector3 direction = orbit.transform.position + (toParent.normalized * (2 * a));

        Vector3 bSide = orbit.GetSpaceVelocityVector(toParent, orbit.OrbitDirection);
        Vector3 leftB = orbit.transform.position + (bSide.normalized * b);
        Vector3 rightB = orbit.transform.position - (bSide.normalized * b);

        Vector3 leftDirB = direction + (bSide.normalized * b);
        Vector3 rightDirB = direction - (bSide.normalized * b);

        Vector3 centerLeft = center + (bSide.normalized * b);
        Vector3 centerRight = center - (bSide.normalized * b);

        Handles.DrawBezier(orbit.transform.position, centerLeft, leftB, centerLeft, Color.white, null, 2);
        Handles.DrawBezier(centerLeft, direction, leftDirB, direction, Color.white, null, 2);

        Handles.DrawBezier(orbit.transform.position, centerRight, rightB, centerRight, Color.white, null, 2);
        Handles.DrawBezier(centerRight, direction, rightDirB, direction, Color.white, null, 2);

        //Scale

        EditorGUI.BeginChangeCheck();
        Handles.color = Color.red;
        float scaleLeftB = Handles.ScaleValueHandle(orbit.Eccentricity, centerLeft,
            Quaternion.LookRotation(centerLeft - center), 1, Handles.ArrowHandleCap, 0.01f);
        scaleLeftB -= 0.5f;

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "B scale");
            float delta = scaleLeftB - lastScale;
            lastScale = scaleLeftB;

            if ((b + Mathf.Sign(-delta) * 0.01f) < a)
            {
                b += Mathf.Sign(-delta) * 0.01f;
                float c = Mathf.Sqrt((a * a) - (b * b));
                float eccentricity = c / a;
                orbit.Eccentricity = eccentricity;
            }
            else orbit.Eccentricity = 0;
        }

        EditorGUI.BeginChangeCheck();
        Handles.color = Color.red;
        float scaleRightB = Handles.ScaleValueHandle(orbit.Eccentricity, centerRight,
            Quaternion.LookRotation(centerRight - center), 1, Handles.ArrowHandleCap, 0.01f);
        scaleRightB -= 0.5f;

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "B scale");
            float delta = scaleRightB - lastScale;
            lastScale = scaleRightB;

            if ((b + Mathf.Sign(delta) * 0.01f) < a)
            {
                b += Mathf.Sign(delta) * 0.01f;
                float c = Mathf.Sqrt((a * a) - (b * b));
                float eccentricity = c / a;
                orbit.Eccentricity = eccentricity;
            }
            else orbit.Eccentricity = 0;
        }

        EditorGUI.BeginChangeCheck();
        Handles.color = Color.red;
        float scaleA = Handles.ScaleValueHandle(orbit.Eccentricity, direction,
            Quaternion.LookRotation(direction - center), 1, Handles.ArrowHandleCap, 0.01f);
        scaleA -= 0.5f;

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "A scale");
            float delta = scaleA - lastScale;
            lastScale = scaleA;

            if ((a + Mathf.Sign(-delta) * 0.01f) > b)
            {
                a += Mathf.Sign(-delta) * 0.01f;
                float c = Mathf.Sqrt((a * a) - (b * b));
                float eccentricity = c / a;
                orbit.Eccentricity = eccentricity;
            }
        }
    }
}
