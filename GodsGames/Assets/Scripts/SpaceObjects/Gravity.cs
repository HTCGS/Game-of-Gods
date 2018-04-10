using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceEngine;

[RequireComponent(typeof(Rigidbody))]
public class Gravity : MonoBehaviour
{
    public static bool Taken = false;

    public Rigidbody rb;
    public static List<Gravity> GravityObjects = new List<Gravity>();

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //Vector3 direction = Vector3.zero;
        
        //foreach (Gravity item in GravityObjects)
        {
            //if (item != this)
            {
                //AddGravityForce(item);

                //Vector3 toTarget = (item.transform.position - this.transform.position).normalized;
                //toTarget *= SpaceMath.GetGravityForce(rb, item.rb, toTarget);
                //direction += toTarget;
            }
        }
        //rb.position += Vector3.one * Time.deltaTime;
        //rb.AddForce(Vector3.one);

        //rb.AddForce(direction * SpaceMath.Mult);
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
            GravityObjects.Remove(this);
            Destroy(gameObject);
            return;
        }

        if (Taken)
        {
            if (this.rb.mass > collision.gameObject.GetComponent<Rigidbody>().mass)
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
        //float rnd = Random.Range(0.2f, 0.5f);
        //to.transform.localScale += from.transform.localScale * rnd;
        //to.GetComponent<Rigidbody>().mass += from.gameObject.GetComponent<Rigidbody>().mass;
        //if (Random.Range(0f, 100f) < 30)
        //{
        //    from.transform.localScale -= from.transform.localScale * rnd;
        //    from.GetComponent<Rigidbody>().mass -= from.gameObject.GetComponent<Rigidbody>().mass * rnd;
        //    return;
        //}

        to.GetComponent<Rigidbody>().mass += from.gameObject.GetComponent<Rigidbody>().mass;
        float r = Mathf.Pow(to.GetComponent<Rigidbody>().mass / (4.18f * 2333f), 1f / 3f) * 2;
        to.transform.localScale = new Vector3(r, r, r);


        Destroy(from);

        //Debug.Log("ooups!");
    }

    public void AddGravityForce(Gravity other)
    {
        SpaceMath.AddGravityForce(rb, other.rb, SpaceMath.Mult);
    }
}
