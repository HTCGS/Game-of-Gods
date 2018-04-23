using UnityEngine;
using SpaceEngine;

[RequireComponent(typeof(Rigidbody))]
[ExecuteInEditMode]
public class Star : MonoBehaviour
{
    public StarClass Class;
    public StarEvolutionState Evolution;

    [Space(5)]
    public GameObject PlanetPrefab;

    [HideInInspector]
    public StarData Data;

    [HideInInspector]
    public PlanetPosition planetPosition;

    void Start()
    {
        if (planetPosition == null)
        {
            planetPosition = new PlanetPosition(gameObject);
            planetPosition.GenerateSeed();
        }
        if (Data == null) return;
        if (Data.IsEmpty()) Create();
        else Visualize();
    }

    void Update()
    {
    }

    public void Create()
    {
        if (Class == StarClass.Random) Data = StarData.GetData();
        else Data = StarData.GetData(Class);
        Data.Mutate();
        float index = Random.Range(0.0f, 1f);
        Data.Radius.RandomValue(index, Mathf.Lerp);
        Data.Color.RandomValue(index, Color.Lerp);
        Data.Mass.RandomValue(index, Mathf.Lerp);
        this.Evolution = Data.EvolutionState;
        this.Class = Data.StarClass;
        Visualize();
        DestroyPlanets(); 
    }

    public void Evolve()
    {
        StarEvolution starEvolution = new StarEvolution(Data);
        Data = starEvolution.Evolve();
        this.Evolution = Data.EvolutionState;
        Visualize();
    }

    public void AddPlanet()
    {
        float position = planetPosition.NextPosition();
        if (position != 0)
        {
            float radius = ((SpaceMath.AU * position) / SpaceMath.Unit);
            Vector3 pos = new Vector3(1, 0, 0).normalized * radius;
            GameObject planet = Instantiate(PlanetPrefab, this.transform.position + pos, Quaternion.identity);
            planet.transform.SetParent(this.transform);
        }
    }

    public void DestroyPlanets()
    {
        for (int i = this.transform.childCount - 1; i >= 0; i--)
        {
            if (Application.isEditor) DestroyImmediate(this.transform.GetChild(i).gameObject);
            else Destroy(this.transform.GetChild(i).gameObject);
        }
        planetPosition.GenerateSeed();
    }

    private void Visualize()
    {
        float radius = Data.Radius.Value;
        radius *= (SpaceMath.SolRadius / SpaceMath.Unit) * 2f;
        this.transform.localScale = new Vector3(radius, radius, radius);
        this.GetComponent<Renderer>().sharedMaterial = new Material(Shader.Find("Standard"));
        this.GetComponent<Renderer>().sharedMaterial.color = Data.Color.Value;
        this.GetComponent<Rigidbody>().mass = Data.Mass.Value * SpaceMath.SolMass * SpaceMath.ToEngineMass;
    }
}