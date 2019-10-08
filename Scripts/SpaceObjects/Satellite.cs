using SpaceEngine;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[ExecuteInEditMode]
public class Satellite : MonoBehaviour
{
    [HideInInspector]
    public PlanetData Data;

    void Start()
    {
        if (Data == null) return;
        if (Data.IsEmpty()) Create();
        else Visualize();
    }

    void Update() { }

    public void Create()
    {
        Create(15000000);
    }

    public void Create(float parentRadius)
    {
        PlanetClass satClass = PlanetClass.Random;
        int randClass = Random.Range(0, 3);
        if (randClass == 0) satClass = PlanetClass.A;
        if (randClass == 1) satClass = PlanetClass.C;
        if (randClass == 2) satClass = PlanetClass.D;
        Data = PlanetData.GetData(satClass);
        Data.Radius.From = 10000f;
        if (parentRadius >= 15000000) Data.Radius.To = 5000000f;
        else if (parentRadius >= 5000000) Data.Radius.To = 2000000f;
        else Data.Radius.To = 700000f;
        float index = Random.Range(0.0f, 1f);
        Data.Radius.RandomValue(index, Mathf.Lerp);
        index = Random.Range(0.0f, 1f);
        Data.Density.RandomValue(index, Mathf.Lerp);
        Visualize();
    }

    private void Visualize()
    {
        float radius = (Data.Radius.Value / SpaceMath.Unit) * 2f;
        this.transform.localScale = new Vector3(radius, radius, radius);
        this.GetComponent<Renderer>().sharedMaterial = new Material(Shader.Find("Standard"));
        this.GetComponent<Rigidbody>().mass = SpaceMath.Shape.SphereMass(Data.Radius.Value, Data.Density.Value) * SpaceMath.ToEngineMass;
    }
}