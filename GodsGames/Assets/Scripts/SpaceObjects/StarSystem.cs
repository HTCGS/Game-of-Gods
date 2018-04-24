using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceEngine;

[ExecuteInEditMode]
public class StarSystem : MonoBehaviour
{
    public GameObject StarPrefab;
    public GameObject PlanetPrefab;

    [Space(5)]
    public float DistanceMult = 1;

    public int Pos;
    public int Childs;

    private FRange ecoZone;

    void Start()
    {
        ecoZone = new FRange();
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
        Star.EcosphereZone(star);

        PlanetPosition planetPosition = new PlanetPosition(star);
        planetPosition.GenerateSeed();
        List<float> positions = planetPosition.Positions();

        Pos = positions.Count;

        float creationChance = (1f / positions.Count) * 3f;
        foreach (var position in positions)
        {
            //if (Random.Range(0f, 1f) < creationChance)
            //if (Random.Range(0f, 1f) < 0.4f)
            {
                float radius = ((SpaceMath.AU * position) / SpaceMath.Unit);
                Vector3 pos = new Vector3(1, 0, 0).normalized * radius * DistanceMult;
                GameObject planet = Instantiate(PlanetPrefab, this.transform.position + pos, Quaternion.identity);
                planet.transform.SetParent(star.transform);
                Zone planetZone = PlanetPositionZone(position);
                planet.GetComponent<Planet>().Create(planetZone);
            }
        }
        Childs = star.transform.childCount;
    }

    public void DestroyStarSystem()
    {
        for (int i = this.transform.childCount - 1; i >= 0; i--)
        {
            if (Application.isEditor) DestroyImmediate(this.transform.GetChild(i).gameObject);
            else Destroy(this.transform.GetChild(i).gameObject);
        }
    }

    private Zone PlanetPositionZone(float planetOrbitRadius)
    {
        if (planetOrbitRadius < ecoZone.From) return Zone.Hot;
        if (planetOrbitRadius > ecoZone.From 
            && planetOrbitRadius < ecoZone.To) return Zone.Eco;
        return Zone.Cold;
    }
}
