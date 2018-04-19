using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceEngine;

public class StarSystem : MonoBehaviour
{
    public GameObject StarPrefab;
    public GameObject PlanetPrefab;

    [Space(5)]
    public float DistanceMult = 1;

    void Start()
    {
        if (this.transform.childCount == 0) Create();
    }

    void Update()
    {
    }

    public void Create()
    {
        GameObject star = Instantiate(StarPrefab, this.transform.position, Quaternion.identity);
        star.transform.SetParent(this.transform);
        star.GetComponent<Star>().Create();

        float d = 0;
        float posIndex = MaxPositionIndex(star.GetComponent<Rigidbody>().mass);
        float creationChance = (1f / posIndex) * 2;
        for (int i = 0; i < posIndex; i++)
        {
            float radius = (d + 4) / 10;
            if (Random.Range(0f, 1f) < creationChance)
            {
                Vector3 pos = (new Vector3(1, 0, 0).normalized * ((SpaceMath.AU * radius) / SpaceMath.Unit)) * DistanceMult;
                GameObject planet = Instantiate(PlanetPrefab, this.transform.position + pos, Quaternion.identity);
                planet.transform.SetParent(star.transform);
            }
            d += 3;
        }
    }

    public void DestroyStarSystem()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if(Application.isEditor)
            {
                DestroyImmediate(this.transform.GetChild(i).gameObject);

            }
            else Destroy(this.transform.GetChild(i).gameObject);
        }
    }

    private int MaxPositionIndex(float starMass)
    {
        int index = 0;
        float lowGravity = 4.77f * Mathf.Pow(10, -30);
        float gravityForce = 0;
        float d = -3;
        do
        {
            index++;
            d += 3;
            float orbitRadius = (d + 4) / 10;
            orbitRadius *= SpaceMath.AU;
            gravityForce = SpaceMath.GetGravityForce(starMass, 1f, orbitRadius);
        } while (gravityForce > lowGravity);
        return index;
    }
}
