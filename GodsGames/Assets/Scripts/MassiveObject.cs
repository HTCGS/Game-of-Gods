using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceEngine;

[RequireComponent(typeof(Rigidbody))]
public class MassiveObject : MonoBehaviour
{
    public List<Rigidbody> Satellites;

    private Rigidbody rb;

    void Start ()
    {
        Satellites = FindMassObjects(gameObject);
        rb = this.GetComponent<Rigidbody>();
	}
	
	void FixedUpdate ()
    {
        foreach (var sat in Satellites)
        {
            SpaceMath.AddGravityForce(sat, rb);
        }
	}

    public static List<Rigidbody> FindMassObjects(GameObject root)
    {
        List<Rigidbody> massObjects = new List<Rigidbody>();
        for (int i = 0; i < root.transform.childCount; i++)
        {
            GameObject child = root.transform.GetChild(i).gameObject;
            Rigidbody childRB = child.GetComponent<Rigidbody>();
            if(childRB != null)
            {
                massObjects.Add(childRB);
            }
            massObjects.InsertRange(massObjects.Count, FindMassObjects(child));
        }
        return massObjects;
    }
}
