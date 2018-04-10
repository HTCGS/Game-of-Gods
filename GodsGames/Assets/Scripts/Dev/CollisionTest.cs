using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceEngine;

public class CollisionTest : MonoBehaviour
{
    public static bool Taken = false;

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

    public float Force;

    public int time;

    private void Start()
    {
        time = 0;
        //Debug.Log(Mathf.Pow(1 / (4.18f * 2333), 1f / 3f) * 2);
    }

    private void FixedUpdate()
    {
        //float starForce = SpaceMath.GetGravityForce(Star, gameObject);
        //float planetForce = SpaceMath.GetGravityForce(Planet, gameObject);

        //Force = SpaceMath.GetGravityForce(this.gameObject, Star);

        //Debug.Log("Star: " + starForce);
        //Debug.Log("Planet: " + planetForce);


        time++;

        if (time == 5)
        {
            time = 0;

            foreach (Gravity to in Gravity.GravityObjects)
            {

                foreach (Gravity item in Gravity.GravityObjects)
                {
                    if (to != item) SpaceMath.AddGravityForce(to.rb, item.rb, SpaceMath.Mult);

                    //Vector3 toTarget = (item.transform.position - this.transform.position).normalized;
                    //toTarget *= SpaceMath.GetGravityForce(rb, item.rb, toTarget);
                    //direction += toTarget;
                }
            }

        }

    }

}
