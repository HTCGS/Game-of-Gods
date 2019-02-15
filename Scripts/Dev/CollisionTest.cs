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

        if (time == 20)
        {
            time = 0;

            for (int i = 0; i < Gravity.GravityObjects.Count - 1; i++)
            {
                for (int j = i + 1; j < Gravity.GravityObjects.Count; j++)
                {
                    Vector3 direction = Gravity.GravityObjects[j].rb.position - Gravity.GravityObjects[i].rb.position;
                    float force = SpaceMath.GetGravityForce(Gravity.GravityObjects[i].rb, Gravity.GravityObjects[j].rb, direction) * SpaceMath.Mult;
                    direction = direction.normalized * force * 20;
                    Gravity.GravityObjects[i].rb.AddForce(direction);
                    Gravity.GravityObjects[j].rb.AddForce(-direction);
                }
            }
        }
    }

}
