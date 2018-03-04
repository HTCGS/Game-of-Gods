using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceEngine;

[RequireComponent(typeof(Rigidbody))]
public class Orbit : MonoBehaviour
{
    public GameObject Parent;

    public Rotation OrbitDirection;

    public float Eccentricity = 0f;
    public bool RandomEccentricity = false;
    public Range<float> EccentricityRange;

    private float OrbitForce;
    private float forceParam;

    private bool start;
    private Rigidbody rb;

    void Start()
    {
        if (Parent == null) Parent = gameObject.transform.parent.gameObject;
        Vector3 toParentDirection = Parent.transform.position - this.transform.position;
        OrbitForce = Mathf.Sqrt(SpaceMath.G * Parent.GetComponent<Rigidbody>().mass / (toParentDirection.magnitude * SpaceMath.Unit)) * SpaceMath.Mult;

        float SecondsForce = Mathf.Sqrt(2) * OrbitForce;
        float deltaForce = (SecondsForce - OrbitForce) * 0.98f;

        if (RandomEccentricity)
        {
            OrbitForce += Random.Range(deltaForce * EccentricityRange.From, deltaForce * EccentricityRange.To);
        }
        else
        {
            OrbitForce += deltaForce * Eccentricity;
        }

        forceParam = ForceParametr(SpaceMath.Mult, SpaceMath.Unit);
        rb = GetComponent<Rigidbody>();
        start = true;
    }

    void FixedUpdate()
    {
        if (start)
        {
            Vector3 toParentDirection = Parent.transform.position - this.transform.position;
            Vector3 forceVector = GetSpaceVelocityVector(toParentDirection, OrbitDirection);
            rb.AddForce(forceVector * (OrbitForce * forceParam), ForceMode.Acceleration);
            start = false;

            List<Rigidbody> sats = MassiveObject.FindMassObjects(gameObject);
            foreach (var sat in sats)
            {
                Rigidbody satRB = sat.GetComponent<Rigidbody>();
                satRB.AddForce(forceVector * (OrbitForce * forceParam), ForceMode.Acceleration);
            }
        }
        Destroy(this);
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

    private Vector3 GetSpaceVelocityVector(Vector3 toParentVector, Rotation direction)
    {
        Vector3 toCrossVector = Vector3.zero;
        switch (direction)
        {
            case Rotation.Parent:
                break;
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

        return Vector3.Cross(toParentVector, toCrossVector).normalized;
    }
}