using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class DevFunctions : MonoBehaviour
{
    public List<GameObject> BigObjects = new List<GameObject>();

    public GameObject GameObject;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddForce()
    {
        GameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 100);
    }

    public void CalculateBigObjects()
    {
        for (int i = 0; i < GravityManager.Objects.Count; i++)
        {
            RaycastHit[] hits = Physics.SphereCastAll(GravityManager.Objects[i].position, 1.5f, Vector3.one);
            if (hits.Length > 3)
            {
                GameObject obj = hits[0].transform.gameObject;
                if (!this.BigObjects.Contains(obj))
                {
                    this.BigObjects.Add(obj);
                }
            }
        }
    }
}