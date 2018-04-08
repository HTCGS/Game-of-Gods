using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceEngine;

[RequireComponent(typeof(Rigidbody))]
public class Gravity : MonoBehaviour
{
    public static bool Taken = false;

    private Rigidbody rb;
    private static List<Gravity> GravityObjects = new List<Gravity>();

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        foreach (Gravity item in GravityObjects)
        {
            if (item != this)
            {
                AddGravityForce(item);
            }
        }
    }

    private void OnEnable()
    {
        GravityObjects.Add(this);
    }

    private void OnDisable()
    {
        GravityObjects.Remove(this);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (this.transform.parent == null) return;
        if (collision.gameObject == this.transform.parent.gameObject)
        {
            Destroy(gameObject);
            return;
        }

        if (Taken)
        {
            if(this.rb.mass > collision.gameObject.GetComponent<Rigidbody>().mass)
            {
                CompressObjects(gameObject, collision.gameObject);
            }
            else CompressObjects(collision.gameObject, gameObject);
            Taken = false;
        }
        else Taken = true;
    }

    private void CompressObjects(GameObject to, GameObject from)
    {
        //float rnd = Random.Range(0.2f, 0.6f);
        //to.transform.localScale += from.transform.localScale * rnd;
        //to.GetComponent<Rigidbody>().mass += from.gameObject.GetComponent<Rigidbody>().mass * rnd;
        //if(Random.Range(0f, 100f) < 30)
        //{
        //    from.transform.localScale -= from.transform.localScale * rnd;
        //    from.GetComponent<Rigidbody>().mass -= from.gameObject.GetComponent<Rigidbody>().mass * rnd;
        //    return;
        //}

        to.transform.localScale += from.transform.localScale;
        to.GetComponent<Rigidbody>().mass += from.gameObject.GetComponent<Rigidbody>().mass;
        Destroy(from);
        Debug.Log("ooups!");
    }

    public void AddGravityForce(Gravity other)
    {
        SpaceMath.AddGravityForce(rb, other.rb, SpaceMath.Mult);
    }
}
