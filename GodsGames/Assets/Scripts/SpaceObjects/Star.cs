using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceEngine;

[RequireComponent(typeof(Rigidbody))]
[ExecuteInEditMode]
public class Star : MonoBehaviour
{
    public StarClass Class;
    public StarEvolutionState Evolution;

    //[HideInInspector]
    public StarData Data;

    void Start()
    {
        if (Data.IsEmpty()) Create();
        else Visualize();
    }

    void Update()
    {
    }

    public void Create()
    {
        if (Class == StarClass.Random) Data = StarData.GetStarData();
        else Data = StarData.GetStarData(Class);
        Data.Mutate();
        float index = Random.Range(0.0f, 1f);
        Data.Radius.RandomValue(index, Mathf.Lerp);
        Data.Color.RandomValue(index, Color.Lerp);
        Data.Mass.RandomValue(index, Mathf.Lerp);
        this.Evolution = Data.EvolutionState;
        this.Class = Data.StarClass;
        Visualize();
    }

    public void Evolve()
    {
        StarEvolution starEvolution = new StarEvolution(Data);
        Data = starEvolution.Evolve();
        this.Evolution = Data.EvolutionState;
        Visualize();
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