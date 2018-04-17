using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceEngine;

[RequireComponent(typeof(Rigidbody))]
public class MultyOrbit : MonoBehaviour
{
    public float Force;

    public GameObject Sat;

    void Start()
    {
        Force = 0;

        GetComponent<Rigidbody>().mass = SpaceMath.SolMass * SpaceMath.ToEngineMass;

        //Debug.Log(4f * Mathf.Pow(10, -27));
    }

    void Update()
    {
        Force = SpaceMath.GetGravityForce(gameObject, Sat);
    }
}
