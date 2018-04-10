using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstMatter : MonoBehaviour
{
    public GameObject Prefab;

    public float Radius;

    public int Sources;

    public int ObjectNum;

    public bool IsRegularShape;

    private GameObject[] Objects;
    private Vector3[] sourcePositions;

    public bool start = false;

    void Start()
    {
        sourcePositions = new Vector3[Sources];
        Objects = new GameObject[ObjectNum * Sources];

        sourcePositions[0] = this.transform.position;
        for (int i = 1; i < Sources; i++)
        {
            //Vector3 dir = Random.insideUnitSphere * Random.Range(0, 2 * Radius);
            Vector2 shift = Random.insideUnitCircle * Random.Range(0, 2 * Radius);
            Vector3 dir = new Vector3(shift.x, 0, shift.y);
            int from = Random.Range(0, i);
            sourcePositions[i] = sourcePositions[from] + dir;
        }

        for (int i = 0; i < Sources; i++)
        {
            for (int j = ObjectNum * i; j < ObjectNum * (i + 1); j++)
            {
                Objects[j] = Instantiate(Prefab, sourcePositions[i], Quaternion.identity);
                //Objects[j].transform.position += Random.insideUnitSphere * Radius;

                //Vector3 pos = Random.insideUnitSphere * Radius;
                //if (!IsRegularShape) pos += Random.insideUnitSphere * Random.Range(0, Radius);
                //Objects[j].transform.position += pos;

                Vector2 circlePoints = Random.insideUnitCircle;
                if(!IsRegularShape) circlePoints += Random.insideUnitCircle * Random.Range(0, Radius);
                //Objects[j].transform.position += new Vector3(circlePoints.x, (0.01f * Random.Range(-1, 1)), circlePoints.y) * Radius;
                //Objects[j].transform.position += new Vector3(circlePoints.x, (0.01f * j), circlePoints.y) * Radius;
                Objects[j].transform.position += new Vector3(circlePoints.x, 0, circlePoints.y) * Radius;

                Objects[j].transform.SetParent(this.transform);
                //float mass = Random.Range(10, 300);
                float mass = Random.Range(1, 10);
                //float mass = 1;
                Objects[j].GetComponent<Rigidbody>().mass = mass;
                //Objects[j].transform.localScale = Vector3.Lerp(new Vector3(0.01f, 0.01f, 0.01f), new Vector3(0.1f, 0.1f, 0.1f), ((mass - 10) / 290));
                //Objects[j].transform.localScale = Vector3.Lerp(new Vector3(0.01f, 0.01f, 0.01f), new Vector3(0.1f, 0.1f, 0.1f), ((mass - 1) / 10));

                float r = Mathf.Pow(mass / (4.18f * 2333), 1f/ 3f) * 2;
                Objects[j].transform.localScale = new Vector3(r, r, r);
                //Objects[j].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            }
        }
        this.GetComponent<MassiveObject>().enabled = true;
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
                Objects[i].GetComponent<Gravity>().enabled = true;
            }
        }
    }
}
