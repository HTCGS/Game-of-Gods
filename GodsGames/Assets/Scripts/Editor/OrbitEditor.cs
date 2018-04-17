using System;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Orbit)), CanEditMultipleObjects]
public class OrbitEditor : Editor
{
    private float lastScale;

    private void OnEnable()
    {
        lastScale = 0;
    }

    private void OnSceneGUI()
    {
        Orbit orbit = target as Orbit;

        if (orbit.Eccentricity < 0 || orbit.Eccentricity >= 1) return;
        if (orbit.transform.parent == null || orbit.Parent == null) return;

        float focus = orbit.Eccentricity;
        Vector3 toParent = orbit.Parent.transform.position - orbit.transform.position;
        float a = toParent.magnitude / (1 - focus);
        float b = Mathf.Sqrt(1 - Mathf.Pow(orbit.Eccentricity, 2)) * a;

        Vector3 center = orbit.transform.position + (toParent.normalized * a);
        Vector3 direction = orbit.transform.position + (toParent.normalized * (2 * a));

        Vector3 bSide = orbit.GetSpaceVelocityVector(toParent, orbit.OrbitDirection).normalized;
        Vector3 leftB = orbit.transform.position + (bSide * b);
        Vector3 rightB = orbit.transform.position - (bSide * b);

        Vector3 leftDirB = direction + (bSide * b);
        Vector3 rightDirB = direction - (bSide * b);

        Vector3 centerLeft = center + (bSide * b);
        Vector3 centerRight = center - (bSide * b);

        Handles.DrawBezier(orbit.transform.position, centerLeft, leftB, centerLeft, Color.white, null, 2);
        Handles.DrawBezier(centerLeft, direction, leftDirB, direction, Color.white, null, 2);

        Handles.DrawBezier(orbit.transform.position, centerRight, rightB, centerRight, Color.white, null, 2);
        Handles.DrawBezier(centerRight, direction, rightDirB, direction, Color.white, null, 2);

        //Scale

        float capSize = toParent.magnitude;
        if (capSize > 3) capSize /= 2;

        EditorGUI.BeginChangeCheck();
        Handles.color = Color.red;
        float scaleLeftB = Handles.ScaleValueHandle(orbit.Eccentricity, centerLeft,
            Quaternion.LookRotation(centerLeft - center), capSize, Handles.CubeHandleCap, 0.01f);
        scaleLeftB -= 0.5f;

        if (EditorGUI.EndChangeCheck())
        {
            float angle = Vector3.Angle(-Vector3.forward, centerLeft - center);
            if (angle < 45) scaleLeftB *= -1;
            angle = Vector3.Angle(-Vector3.right, centerLeft - center);
            if (angle < 45) scaleLeftB *= -1;

            Undo.RecordObject(target, "Left B scale");
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
            Quaternion.LookRotation(centerRight - center), capSize, Handles.CubeHandleCap, 0.01f);
        scaleRightB -= 0.5f;

        if (EditorGUI.EndChangeCheck())
        {
            float angle = Vector3.Angle(Vector3.forward, centerRight - center);
            if (angle < 45) scaleRightB *= -1;
            angle = Vector3.Angle(Vector3.right, centerRight - center);
            if (angle < 45) scaleRightB *= -1;

            Undo.RecordObject(target, "Right B scale");
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
            Quaternion.LookRotation(direction - center), capSize, Handles.CubeHandleCap, 0.01f);
        scaleA -= 0.5f;

        if (EditorGUI.EndChangeCheck())
        {
            float angle = Vector3.Angle(Vector3.forward, direction - center);
            if (angle < 45) scaleA *= -1;
            angle = Vector3.Angle(Vector3.right, direction - center);
            if (angle < 45) scaleA *= -1;

            Undo.RecordObject(target, "A scale");
            float delta = scaleA - lastScale;
            lastScale = scaleA;

            if ((a + Mathf.Sign(-delta) * 0.01f) >= b)
            {
                a += Mathf.Sign(-delta) * 0.01f;
                float c = Mathf.Sqrt((a * a) - (b * b));
                float eccentricity = c / a;
                orbit.Eccentricity = eccentricity;
            }
        }
    }
}
