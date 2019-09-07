using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DisplayOrbit : MonoBehaviour
{
    private LineRenderer LineRend;
    private int PointsIndex;

    private float ObjectRadius;

    public float Eccentricity;
    public float A;
    public float B;
    private Vector3 LD;
    private Vector3 LU;
    private Vector3 RU;
    private Vector3 RD;

    void Start()
    {
        LineRend = GetComponent<LineRenderer>();
        LineRend.SetPosition(0, this.transform.position);
        PointsIndex = 0;
        ObjectRadius = this.transform.localScale.x / 2;
        // LineRend.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        LineRend.material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

        LD = this.transform.position;
        LU = this.transform.position;
        RU = this.transform.position;
        RD = this.transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 lastPoint = LineRend.GetPosition(PointsIndex);
        if (Vector3.Distance(lastPoint, this.transform.position) >= ObjectRadius)
        {
            PointsIndex++;
            Vector3[] points = new Vector3[LineRend.positionCount];
            LineRend.GetPositions(points);
            LineRend.positionCount = PointsIndex + 1;
            LineRend.SetPositions(points);
            LineRend.SetPosition(PointsIndex, this.transform.position);
        }
        // GetEccentricity();
    }

    private void GetEccentricity()
    {
        Vector3[] points = new Vector3[LineRend.positionCount];
        LineRend.GetPositions(points);

        Vector3 up = points.Where(v => v.z == points.Max(p => p.z)).First();
        Vector3 down = points.Where(v => v.z == points.Min(p => p.z)).First();
        Vector3 left = points.Where(v => v.x == points.Min(p => p.x)).First();
        Vector3 right = points.Where(v => v.x == points.Max(p => p.x)).First();

        B = (up - down).magnitude / 2;
        A = (left - right).magnitude / 2;

        if (B > A)
        {
            float tmp = B;
            B = A;
            A = tmp;
        }
        Eccentricity = Mathf.Sqrt(Mathf.Abs(1 - ((B * B) / (A * A))));

        // Debug.DrawLine(LU, RU, Color.white);
        // Debug.DrawLine(RU, RD, Color.white);
        // Debug.DrawLine(RD, LD, Color.white);
        // Debug.DrawLine(LD, LU, Color.white);
    }
}