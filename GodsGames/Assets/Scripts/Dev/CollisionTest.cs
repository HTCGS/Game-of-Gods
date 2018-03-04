using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceEngine;

public class CollisionTest : MonoBehaviour
{
    public static bool Taken = false;

    private void OnCollisionStay(Collision collision)
    {
        if (CollisionTest.Taken)
        {
            Destroy(collision.gameObject);
            Taken = false;
        }
        else Taken = true;
    }

    public GameObject Star;
    public GameObject Planet;

    public int Sum;

    public int[] nums = new int[10];

    private void FixedUpdate()
    {
        //float starForce = SpaceMath.GetGravityForce(Star, gameObject);
        //float planetForce = SpaceMath.GetGravityForce(Planet, gameObject);

        //Debug.Log("Star: " + starForce);
        //Debug.Log("Planet: " + planetForce);


        //Debug.Log(Random.Range(0.00001f, 1f));
        //Debug.Log(Random.value * (1f - 0.00001f) + 0.00001f);
        //Debug.Log(Random.Range(0.00001f, 0.001f));

        //float rnd = Random.Range(0.00001f, 80f);

        //if (rnd >= 0.00001f && rnd <= 0.00003f) O++;
        //if (rnd > 0.00003f && rnd <= 0.0001f) Super++;
        //if (rnd > 0.0001f && rnd <= 0.1f) B++;
        //if (rnd > 0.1f && rnd <= 0.4f) GiG++;
        //if (rnd > 0.4f && rnd <= 0.7f) A++;
        //if (rnd > 0.7f && rnd <= 2f) F++;
        //if (rnd > 2f && rnd <= 3.5f) G++;
        //if (rnd > 3.5f && rnd <= 5f) Kar++;
        //if (rnd > 5f && rnd <= 8f) K++;
        //if (rnd > 8f && rnd <= 80f) M++;


        //float mult = Random.Range(10f, 100f);
        //float num = Random.Range(0.1f, 2.5f);

        //Debug.Log();

        //if(Random.Range(0.0f, 100) <= 1) Sum++;

        int rand = Random.Range(0, 10);
        if (rand == 0)
        {
            nums[0]++;
        }
        if (rand == 1)
        {
            nums[1]++;
        }
        if (rand == 2)
        {
            nums[2]++;
        }
        if (rand == 3)
        {
            nums[3]++;
        }
        if (rand == 4)
        {
            nums[4]++;
        }
        if (rand == 5)
        {
            nums[5]++;
        }
        if (rand == 6)
        {
            nums[6]++;
        }
        if (rand == 7)
        {
            nums[7]++;
        }
        if (rand == 8)
        {
            nums[8]++;
        }
        if (rand == 9)
        {
            nums[9]++;
        }
    
    }

}
