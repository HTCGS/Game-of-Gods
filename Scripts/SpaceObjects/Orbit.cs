﻿using System.Collections;
using System.Collections.Generic;
using SpaceEngine;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Orbit : MonoBehaviour
{
    public GameObject Parent;

    public Rotation OrbitDirection;

    public float Eccentricity = 0f;
    public bool RandomEccentricity = false;

    private float orbitVelocity;
    private float velocityParam;

    private Rigidbody rb;

    void Start()
    {
        if (gameObject.transform.parent == null) return;
        if (Parent == null) Parent = gameObject.transform.parent.gameObject;
        orbitVelocity = SpaceMath.GetFirstCosmicVelocity(gameObject, Parent, SpaceMath.Mult);

        float SecondVelocity = Mathf.Sqrt(2) * orbitVelocity;
        float deltaForce = (SecondVelocity - orbitVelocity);

        if (RandomEccentricity) orbitVelocity += Random.Range(0f, deltaForce);
        else orbitVelocity += deltaForce * Eccentricity;

        velocityParam = ForceParametr(SpaceMath.Mult, SpaceMath.Unit);

        Vector3 toParentDirection = Parent.transform.position - this.transform.position;
        Vector3 velocityVector = GetOrbitalRotationVector(toParentDirection, OrbitDirection);
        velocityVector *= orbitVelocity * velocityParam;

        List<Rigidbody> sats = MassiveObject.FindMassObjects(gameObject);
        foreach (var sat in sats)
        {
            Rigidbody satRB = sat.GetComponent<Rigidbody>();
            satRB.AddForce(velocityVector, ForceMode.VelocityChange);
        }

        rb = GetComponent<Rigidbody>();
        rb.AddForce(velocityVector, ForceMode.VelocityChange);

        Vector3 ppp = this.transform.position - Parent.transform.position;
        Vector3 end = Quaternion.AngleAxis(25f, Vector3.down) * ppp;
        print(end);

        end = Quaternion.AngleAxis(25f, Vector3.down) * ppp;
        print(end);

        end = Quaternion.AngleAxis(25f, Vector3.down) * ppp;
        print(end);

        Destroy(this);
    }

    void FixedUpdate()
    {

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
        if (numSize % 2 == 0) param = 1f;
        else param = 3.169f;

        if (param == 1f) numSize /= 2;
        else numSize = (numSize / 2) + 1;

        for (int i = 0; i < numSize; i++)
        {
            param /= 10;
        }
        return param;
    }

    public void SetParent()
    {
        GameObject parent = gameObject.transform.parent.gameObject;
        while (parent)
        {
            Rigidbody parentRB = parent.GetComponent<Rigidbody>();
            MassiveObject parentMO = parent.GetComponent<MassiveObject>();
            if (parentRB && parentMO)
            {
                this.Parent = parent;
                break;
            }
            parent = parent.transform.parent.gameObject;
        }
    }

    public Vector3 GetOrbitalRotationVector(Vector3 toParentVector, Rotation direction)
    {
        Vector3 toCrossVector = Vector3.zero;
        switch (direction)
        {
            case Rotation.Parent:
                Orbit parentObrit = Parent.GetComponent<Orbit>();
                return GetOrbitalRotationVector(toParentVector, parentObrit.OrbitDirection);
            case Rotation.Right:
                toCrossVector = Parent.transform.right;
                break;
            case Rotation.Left:
                toCrossVector = -Parent.transform.right;
                break;
            case Rotation.Up:
                toCrossVector = Parent.transform.up;
                break;
            case Rotation.Down:
                toCrossVector = -Parent.transform.up;
                break;
            case Rotation.Forward:
                toCrossVector = Parent.transform.forward;
                break;
            case Rotation.Backward:
                toCrossVector = -Parent.transform.forward;
                break;
            default:
                break;
        }
        return -Vector3.Cross(toParentVector, toCrossVector).normalized;
    }
}