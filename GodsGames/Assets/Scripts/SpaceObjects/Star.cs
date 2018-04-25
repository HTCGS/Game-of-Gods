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

    public bool ShowEcoZone;

    [HideInInspector]
    public StarData Data;

    [HideInInspector]
    public PlanetPosition planetPosition;

    [HideInInspector]
    public FRange EcoZone;

    void Start()
    {
        if (planetPosition == null)
        {
            planetPosition = new PlanetPosition(gameObject);
            planetPosition.GenerateSeed();
        }
        else planetPosition.SetParent(gameObject);
        if (Data == null) return;
        if (Data.IsEmpty()) Create();
        else Visualize();
        EcoZone = Star.EcosphereZone(this);
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
        Data.Luminosity.RandomValue(index, Mathf.Lerp);
        Data.Radius.RandomValue(index, Mathf.Lerp);
        Data.Color.RandomValue(index, Color.Lerp);
        Data.Mass.RandomValue(index, Mathf.Lerp);
        this.Evolution = Data.EvolutionState;
        this.Class = Data.StarClass;
        Visualize();
        DestroyPlanets();
        EcoZone = Star.EcosphereZone(this);
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

    public static FRange EcosphereZone(Star star)
    {
        FRange ecoZone = new FRange();
        ecoZone.Value = Mathf.Sqrt(star.Data.Luminosity.Value);
        ecoZone.From = ecoZone.Value * 0.95f;
        ecoZone.To = ecoZone.Value * 1.35f;
        return ecoZone;
    }

    public static FRange EcosphereZone(GameObject star)
    {
        return Star.EcosphereZone(star.GetComponent<Star>());
    }
}