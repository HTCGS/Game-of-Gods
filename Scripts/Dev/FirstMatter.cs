using System.Collections;
using System.Collections.Generic;
using SpaceEngine;
using UnityEditor;
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

    // public GameObject aaaa;

    void Start()
    {
        // string localPath = "Assets/" + "a.prefab";
        // localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

        // PrefabUtility.SaveAsPrefabAsset(aaaa, localPath);
        // for (int i = 0; i < ObjectNum; i++)
        // {
        //     float posX = Random.Range(0f, 1f);
        //     float posY = Random.Range(0f, 1f);
        //     Vector3 pos = new Vector3(Radius * posX, 0, Radius * posY);
        //     pos += this.transform.position;
        //     //Instantiate(Prefab, pos, Quaternion.identity).transform.SetParent(this.transform);
        //     Instantiate(Prefab, this.transform.position, Quaternion.identity).transform.SetParent(this.transform);
        // }

        // Destroy(this);

        sourcePositions = new Vector3[Sources];
        Objects = new GameObject[ObjectNum * Sources];

        sourcePositions[0] = this.transform.position;
        for (int i = 1; i < Sources; i++)
        {
            Vector3 dir = Random.insideUnitSphere * Random.Range(0, 2 * Radius);
            // Vector3 dir = Random.insideUnitSphere * Radius;
            //Vector2 shift = Random.insideUnitCircle * Random.Range(0, 2 * Radius);
            //Vector3 dir = new Vector3(shift.x, 0, shift.y);
            int from = Random.Range(0, i);
            sourcePositions[i] = sourcePositions[from] + dir;
            // sourcePositions[i] = sourcePositions[i - 1] + dir;
        }

        // Objects[0] = Instantiate(Prefab, sourcePositions[0], Quaternion.identity);
        // Objects[0].transform.position += Vector3.forward * Radius;

        // Objects[1] = Instantiate(Prefab, sourcePositions[0], Quaternion.identity);
        // Objects[1].transform.position += -Vector3.forward * Radius;

        // Objects[2] = Instantiate(Prefab, sourcePositions[0], Quaternion.identity);
        // Objects[2].transform.position += Vector3.right * Radius;

        // Objects[3] = Instantiate(Prefab, sourcePositions[0], Quaternion.identity);
        // Objects[3].transform.position += -Vector3.right * Radius;

        for (int i = 0; i < Sources; i++)
        {
            for (int j = ObjectNum * i; j < ObjectNum * (i + 1); j++)
            {
                Objects[j] = Instantiate(Prefab, sourcePositions[i], Quaternion.identity);
                Objects[j].transform.position += Random.insideUnitSphere * Radius;

                // Vector2 rand = Random.insideUnitCircle;
                // Vector2 circlePoints = (rand * Radius) + (rand * 5);
                // Objects[j].transform.position += new Vector3(circlePoints.x, 0, circlePoints.y);

                //         Vector3 pos = Random.insideUnitSphere * Radius;
                //         if (!IsRegularShape) pos += Random.insideUnitSphere * Random.Range(0, Radius);
                //         Objects[j].transform.position += pos;

                //         //Vector2 circlePoints = Random.insideUnitCircle;
                //         //circlePoints += circlePoints.normalized;
                //         //if (!IsRegularShape) circlePoints += Random.insideUnitCircle * Random.Range(0, 0.4f * Radius);
                //         //Objects[j].transform.position += new Vector3(circlePoints.x, 0, circlePoints.y) * Radius;

                //         //Objects[j].transform.position += new Vector3(circlePoints.x, (0.01f * Random.Range(-1, 1)), circlePoints.y) * Radius;
                //         //Objects[j].transform.position += new Vector3(circlePoints.x, (0.01f * j), circlePoints.y) * Radius;

                Objects[j].transform.SetParent(this.transform);
                float mass = Random.Range(1, 100);
                //         //float mass = Random.Range(1, 10);
                //         float mass = 1;
                Objects[j].GetComponent<Rigidbody>().mass = mass;
                //         //Objects[j].transform.localScale = Vector3.Lerp(new Vector3(0.01f, 0.01f, 0.01f), new Vector3(0.1f, 0.1f, 0.1f), ((mass - 10) / 290));
                //         //Objects[j].transform.localScale = Vector3.Lerp(new Vector3(0.01f, 0.01f, 0.01f), new Vector3(0.1f, 0.1f, 0.1f), ((mass - 1) / 10));

                //         float r = Mathf.Pow(mass / (4.18f * 2333), 1f / 3f) * 2;
                //         Objects[j].transform.localScale = new Vector3(r, r, r);
                //         //Objects[j].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            }
        }

        // for (int i = 0; i < Sources; i++)
        // {
        //     int sourcesObjectsNum = Random.Range(0, ObjectNum);
        //     for (int j = 0; j < sourcesObjectsNum; j++)
        //     {
        //         GameObject obj = Instantiate(Prefab, sourcePositions[i], Quaternion.identity);
        //         obj.transform.position += Random.insideUnitSphere * Radius;
        //         obj.transform.SetParent(this.transform);
        //         float mass = Random.Range(1, 100);
        //         obj.GetComponent<Rigidbody>().mass = mass;
        //     }
        // }

        // for (int i = 0; i < ObjectNum; i++)
        // {
        //     Objects[i].GetComponent<Orbit>().enabled = true;
        // }

        // gameObject.GetComponent<MassiveObject>().enabled = true;
        Destroy(this);
        // //this.GetComponent<MassiveObject>().enabled = true;
    }

    void Update()
    {
        // if (start)
        // {
        //     start = false;
        //     ActivateGravity();
        // }

        // Collider[] hits = Physics.OverlapBox(this.transform.position, Vector3.one / 2, Quaternion.identity);

        // if (hits.Length > 0)
        // {

        //     Debug.Log(hits.Length);

        //     foreach (var hit in hits)
        //     {
        //         Debug.Log(hit.transform.position);
        //     }
        // }
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