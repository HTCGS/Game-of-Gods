using System.Collections;
using System.Collections.Generic;
using SpaceEngine;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MassiveObject : MonoBehaviour
{
    public List<Rigidbody> Satellites;

    private Rigidbody rb;

    void Start()
    {
        Satellites = FindMassObjects(gameObject);
        rb = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        foreach (var sat in Satellites)
        {
            if (sat != null)
            {
                SpaceMath.AddGravityForce(sat, rb, SpaceMath.Mult);
                // float gravity = SpaceMath.GetGravityForce(sat, rb) * SpaceMath.Mult * Time.fixedDeltaTime;
                // Vector3 gravityVector = this.transform.position - sat.position;
                // gravityVector *= gravity;
                // sat.velocity += gravityVector;

            }
        }
    }

    public static List<Rigidbody> FindMassObjects(GameObject root)
    {
        List<Rigidbody> massObjects = new List<Rigidbody>();
        for (int i = 0; i < root.transform.childCount; i++)
        {
            GameObject child = root.transform.GetChild(i).gameObject;
            if (child.activeSelf == false) continue;

            Rigidbody childRB = child.GetComponent<Rigidbody>();
            if (childRB != null)
            {
                massObjects.Add(childRB);
            }
            massObjects.InsertRange(massObjects.Count, FindMassObjects(child));
        }
        return massObjects;
    }
}