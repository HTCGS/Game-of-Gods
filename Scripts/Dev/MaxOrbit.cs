using System.Collections;
using System.Collections.Generic;
using SpaceEngine;
using UnityEngine;

public class MaxOrbit : MonoBehaviour
{
    public GameObject Satellite;

    public float MaxDistance;

    private Vector3 MaxOrbitVector;

    void Start()
    {
        float f = 0.0000001f;
        MaxDistance = Mathf.Sqrt(SpaceMath.G * this.GetComponent<Rigidbody>().mass * Satellite.GetComponent<Rigidbody>().mass * Mathf.Pow(f, -1));
        //Debug.Log("Max orbit = " + maxOrbit);
        MaxOrbitVector = this.transform.forward * MaxDistance;
    }

    void FixedUpdate()
    {
        Debug.DrawLine(this.transform.position, this.transform.position + MaxOrbitVector, Color.yellow);

    }
}