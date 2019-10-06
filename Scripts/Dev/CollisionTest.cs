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

    private void Start()
    {
        time = 0;

        Rigidbody rb = GetComponent<Rigidbody>();
        // rb.AddForce(Vector3.forward * 26f);
        // rb.AddForce(Vector3.forward, ForceMode.Acceleration);
        // rb.AddForce(Vector3.forward, ForceMode.Impulse);
        // rb.AddForce(Vector3.forward * 4.61f, ForceMode.VelocityChange);

        pos = this.transform.position;

    }

    private void FixedUpdate()
    {
        // if (time == 25)
        // {
        //     Distance = (this.transform.position - pos).magnitude;
        //     pos = this.transform.position;
        // }
        // time++;

        int[] a = new int[30000];

    }
}