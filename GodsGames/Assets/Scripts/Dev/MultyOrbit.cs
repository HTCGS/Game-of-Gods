using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceEngine;

[RequireComponent(typeof(Rigidbody))]
public class MultyOrbit : MonoBehaviour
{
    public GameObject[] Parents;

    public Vector3[] OrbitForceVector;
    public float[] OrbitForce;

    private float forceParam;

    private Rigidbody rb;
    private Vector3 fVector;

    public int start;

    void Start()
    {
        OrbitForceVector = new Vector3[Parents.Length];
        OrbitForce = new float[Parents.Length];
        forceParam = ForceParametr(SpaceMath.Mult, SpaceMath.Unit);
        for (int i = 0; i < Parents.Length; i++)
        {
            Vector3 toParentDirection = Parents[i].transform.position - this.transform.position;
            OrbitForceVector[i] = Vector3.Cross(toParentDirection, this.transform.up);
            OrbitForce[i] = Mathf.Sqrt(SpaceMath.G * Parents[i].GetComponent<Rigidbody>().mass * Mathf.Pow(toParentDirection.magnitude * SpaceMath.Unit, -1));
            OrbitForce[i] *= SpaceMath.Mult;
        }
        rb = GetComponent<Rigidbody>();
        start = 0;
        fVector = Vector3.zero;
    }

    void FixedUpdate()
    {
        if (start == 0)
        {
            foreach (var vector in OrbitForceVector)
            {
                fVector += vector;
            }
            fVector /= OrbitForceVector.Length;

            float force = 0f;
            foreach (var oForce in OrbitForce)
            {
                force += oForce;
            }
            force /= OrbitForce.Length;

            rb.AddForce(fVector.normalized * (force * forceParam), ForceMode.Acceleration);
            start++;
        }

        //Debug.DrawLine(this.transform.position, this.transform.position + (fVector.normalized * 2), Color.blue);

        foreach (var parent in Parents)
        {
            SpaceMath.AddGravityForce(gameObject, parent);
        }
    }

    private float ForceParametr(float mult, float unit)
    {
        float param = 0f;

        while (unit != 1)
        {
            mult *= 10;
            unit /= 10;
        }

        int numSize = mult.ToString("F").Length - 4;
        if (numSize % 2 == 0) param = 50f;
        else param = 15.8f;

        numSize /= 2;
        for (int i = 0; i < numSize; i++)
        {
            param /= 10;
        }
        return param;
    }
}
