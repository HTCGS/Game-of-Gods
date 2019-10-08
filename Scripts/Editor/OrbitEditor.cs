using System;
using UnityEditor;
using UnityEngine;

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
        if (orbit.Parent == null) orbit.SetParent();
        if (orbit.Parent == null) return;

        float focus = orbit.Eccentricity;
        Vector3 toParent = orbit.Parent.transform.position - orbit.transform.position;
        float a = toParent.magnitude / (1 - focus);
        float b = Mathf.Sqrt(1 - Mathf.Pow(orbit.Eccentricity, 2)) * a;

        Vector3 center = orbit.transform.position + (toParent.normalized * a);
        Vector3 direction = orbit.transform.position + (toParent.normalized * (2 * a));

        Vector3 bSide = orbit.GetOrbitalRotationVector(toParent, orbit.OrbitDirection).normalized;
        if (bSide == Vector3.zero) return;

        Vector3 leftStartCorner = orbit.transform.position + (bSide * b);
        Vector3 rightStartCorner = orbit.transform.position - (bSide * b);

        Vector3 leftDirB = direction + (bSide * b);
        Vector3 rightDirB = direction - (bSide * b);

        Vector3 centerLeft = center + (bSide * b);
        Vector3 centerRight = center - (bSide * b);

        Handles.DrawBezier(orbit.transform.position, centerLeft, leftStartCorner, centerLeft, Color.white, null, 2);
        Handles.DrawBezier(centerLeft, direction, leftDirB, direction, Color.white, null, 2);

        Handles.DrawBezier(orbit.transform.position, centerRight, rightStartCorner, centerRight, Color.white, null, 2);
        Handles.DrawBezier(centerRight, direction, rightDirB, direction, Color.white, null, 2);

        //Direction

        Handles.color = Color.cyan;
        Vector3 start = orbit.transform.position - center;
        Vector3 norm = Vector3.Cross(bSide, toParent);
        start = Quaternion.AngleAxis(25f, norm) * start;
        Handles.DrawWireArc(center, norm, start, 40, b * 1.1f);

        Vector3 arrowPos = center + (bSide * (b * 1.1f));
        arrowPos -= orbit.Parent.transform.position;
        Vector3 end = Quaternion.AngleAxis(-25f, norm) * arrowPos;
        Vector3 end2 = Quaternion.AngleAxis(-26f, norm) * arrowPos;
        end += orbit.Parent.transform.position;
        end2 += orbit.Parent.transform.position;

        float orbitSize = toParent.magnitude;
        float dirCapSize = 5;
        if (orbitSize < 30f) dirCapSize = 4;
        if (orbitSize < 12f) dirCapSize = 2;
        if (orbitSize < 6f) dirCapSize = 1;
        if (orbitSize < 2f) dirCapSize = 0.5f;
        Handles.ArrowHandleCap(0, end2, Quaternion.LookRotation(end - end2), dirCapSize, EventType.Repaint);

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