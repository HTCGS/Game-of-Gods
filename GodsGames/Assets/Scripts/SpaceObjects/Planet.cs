using UnityEngine;
using SpaceEngine;

[RequireComponent(typeof(Rigidbody))]
[ExecuteInEditMode]
public class Planet : MonoBehaviour
{
    public PlanetClass Class;

    [Space(5)]
    public GameObject SatellitePrefab;

    [HideInInspector]
    public PlanetData Data;

    [HideInInspector]
    public SatellitePosition satellitePosition;

    void Start()
    {
        if (satellitePosition == null)
        {
            satellitePosition = new SatellitePosition(gameObject);
            satellitePosition.GenerateSeed();
        }
        else satellitePosition.SetParent(gameObject);
        if (Data == null) return;
        if (Data.IsEmpty()) Create();
        else Visualize();
    }

    void Update()
    {
    }

    public void Create()
    {
        if (Class == PlanetClass.Random) Data = PlanetData.GetData();
        else Data = PlanetData.GetData(Class);
        PlanetValues();
        Visualize();
        DestroySatellites();
    }

    public void Create(Zone zone)
    {
        Data = PlanetData.GetData(zone);
        PlanetValues();
        Visualize();
        DestroySatellites();
    }

    public void AddSatellite()
    {
        if (Data.Radius.Value < 1000f) return;
        float position = satellitePosition.NextPosition();
        if (position != 0)
        {
            float radius = ((100000000f * position) / SpaceMath.Unit) + ((this.transform.lossyScale.x / 2f) * 2.45f);
            Vector3 pos = new Vector3(1, 0, 0) * radius;
            GameObject satellite = Instantiate(SatellitePrefab, this.transform.position + pos, Quaternion.identity);
            satellite.GetComponent<Satellite>().Create(Data.Radius.Value);
            satellite.transform.SetParent(this.transform);
        }
    }

    public void DestroySatellites()
    {
        for (int i = this.transform.childCount - 1; i >= 0; i--)
        {
            if (Application.isEditor) DestroyImmediate(this.transform.GetChild(i).gameObject);
            else Destroy(this.transform.GetChild(i).gameObject);
        }
        satellitePosition.GenerateSeed();
    }

    private void PlanetValues()
    {
        float index = Random.Range(0.0f, 1f);
        Data.Radius.RandomValue(index, Mathf.Lerp);
        index = Random.Range(0.0f, 1f);
        Data.Density.RandomValue(index, Mathf.Lerp);
        this.Class = Data.PlanetClass;
    }

    private void Visualize()
    {
        float radius = (Data.Radius.Value / SpaceMath.Unit) * 2f;
        this.transform.localScale = new Vector3(radius, radius, radius);
        this.GetComponent<Renderer>().sharedMaterial = new Material(Shader.Find("Standard"));
        this.GetComponent<Rigidbody>().mass = SpaceMath.GetSphereMass(Data.Radius.Value, Data.Density.Value) * SpaceMath.ToEngineMass;
    }
}
