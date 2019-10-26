using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SpaceEngine;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    public static bool Taken = false;

    // public Range<float> AAA = new Range<float>(2.24f, 34.47f);
    public FRange AAA = new FRange(2.24f, 34.47f);
    // public CRange CCC = new CRange(Color.blue, Color.magenta);
    // public Range AAA = new FRange(2, 4);
    // public Range<float> AAA = new FRange(2, 4);

    // public Apple<int> apple1 = new Apple<int>();
    // public BigApple apple2 = new BigApple();

    // public Apple<float> ddd = new BigApple();

    // public IFruit<float> bb = new BigApple();

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (CollisionTest.Taken)
    //    {
    //        Destroy(collision.gameObject);
    //        Taken = false;
    //    }
    //    else Taken = true;
    //}

    public GameObject Star;
    public GameObject Planet;

    public int time;

    public Vector3 pos;

    public float Distance;

    public float Force1;
    public float Force2;

    Rigidbody parent;
    Rigidbody rb;

    private void Start()
    {
        time = 0;

        rb = GetComponent<Rigidbody>();
        // rb.AddForce(Vector3.forward * 26f);
        // rb.AddForce(Vector3.forward, ForceMode.Acceleration);
        // rb.AddForce(Vector3.forward, ForceMode.Impulse);
        // rb.AddForce(Vector3.forward * 4.61f, ForceMode.VelocityChange);

        pos = this.transform.position;

        parent = gameObject.transform.parent.GetComponent<Rigidbody>();

        Vector3 L1 = SpaceMath.Orbit.L1Point(parent, rb);
        Vector3 L2 = SpaceMath.Orbit.L2Point(parent, rb);
        Vector3 L3 = SpaceMath.Orbit.L3Point(parent, rb);
        Vector3 L4 = SpaceMath.Orbit.L4Point(parent, rb);

        float distance = SpaceMath.Orbit.L1L2Distance(parent, rb);

        // GameObject obj1 = Instantiate(Planet, L1, Quaternion.identity);
        // GameObject obj2 = Instantiate(Planet, L2, Quaternion.identity);
        // GameObject obj3 = Instantiate(Planet, L3, Quaternion.identity);
        GameObject obj4 = Instantiate(Planet, L4, Quaternion.identity);

        // obj1.transform.SetParent(gameObject.transform);
        // obj2.transform.SetParent(gameObject.transform);
        // obj3.transform.SetParent(gameObject.transform.parent.transform);
        obj4.transform.SetParent(gameObject.transform);

        // GetComponent<Orbit>().enabled = true;
        // GetComponent<MassiveObject>().enabled = true;
        // obj1.GetComponent<Orbit>().enabled = true;
        // obj2.GetComponent<Orbit>().enabled = true;

        print(distance);
        print(L1);
        print(L2);
        print(L3);
        print(SpaceMath.Orbit.SphereOfInfluence(parent, rb));

        print(SpaceMath.Orbit.SphereOfInfluence(SpaceMath.SolMass, SpaceMath.EarthMass, SpaceMath.AU));

    }

    private void FixedUpdate()
    {
        // if (time == 25)
        // {
        //     Distance = (this.transform.position - pos).magnitude;
        //     pos = this.transform.position;
        // }
        // time++;

        // this.Distance = (this.transform.position - Planet.transform.position).magnitude;

        // Force1 = SpaceMath.Gravity.GetGravityForce(parent, rb);

        // Rigidbody parent2 = gameObject.transform.parent.transform.parent.GetComponent<Rigidbody>();
        // Force2 = SpaceMath.Gravity.GetGravityForce(parent2, rb);

    }
}