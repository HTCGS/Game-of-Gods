using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstMatter : MonoBehaviour
{
    public GameObject Prefab;

    public float Radius;

    public int ObjectNum;

    private GameObject[] Objects;

    public bool start = false;

    void Start()
    {
        Objects = new GameObject[ObjectNum];
        for (int i = 0; i < ObjectNum; i++)
        {
            Objects[i] = Instantiate(Prefab, this.transform.position, Quaternion.identity);
            //Objects[i].transform.position += Random.insideUnitSphere * Radius;

            Vector3 pos = Random.insideUnitSphere * Radius;
            pos += Random.insideUnitSphere * Random.Range(0, Radius);
            Objects[i].transform.position += pos;

            //Vector2 circlePoints = Random.insideUnitCircle;
            //Objects[i].transform.position += new Vector3(circlePoints.x, (0.01f * Random.Range(-1, 1)), circlePoints.y) * Radius;
            //Objects[i].transform.position += new Vector3(circlePoints.x, (0.01f * j), circlePoints.y) * Radius;
            //Objects[i].transform.position += new Vector3(circlePoints.x, 0, circlePoints.y) * Radius;
            Objects[i].transform.SetParent(this.transform);
            //float mass = Random.Range(10, 300);
            float mass = Random.Range(1, 10);
            //float mass = 1;
            Objects[i].GetComponent<Rigidbody>().mass = mass;
            //Objects[i].transform.localScale = Vector3.Lerp(new Vector3(0.01f, 0.01f, 0.01f), new Vector3(0.1f, 0.1f, 0.1f), ((mass - 10) / 290));
            Objects[i].transform.localScale = Vector3.Lerp(new Vector3(0.01f, 0.01f, 0.01f), new Vector3(0.1f, 0.1f, 0.1f), ((mass - 1) / 10));
            //Objects[i].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }
    }

    void Update()
    {
        if (start)
        {
            start = false;
            ActivateGravity();
        }
    }

    private void ActivateGravity()
    {
        for (int i = 0; i < Objects.Length; i++)
        {
            if (Objects[i] != null)
            {
                Objects[i].SetActive(true);
            }
        }
    }
}
