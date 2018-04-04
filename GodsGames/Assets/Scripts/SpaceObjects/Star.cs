using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceEngine;

[RequireComponent(typeof(Rigidbody))]
public class Star : MonoBehaviour
{
    public StarClass Class;
    public StarEvolutionState Evolution;

    private StarData star;

    void Start()
    {
        CreateStar();
    }

    void Update()
    {
    }

    public void CreateStar()
    {
        if (Class == StarClass.Random) star = StarData.GetStarData();
        else star = StarData.GetStarData(Class);
        star.Mutate();
        float index = Random.Range(0.0f, 1f);
        float radius = star.Radius.RandomValue(index, Mathf.Lerp);
        radius *= (SpaceMath.SolRadius / SpaceMath.Unit) * 2f;
        this.transform.localScale = new Vector3(radius, radius, radius);
        this.GetComponent<Renderer>().material.color = star.Color.RandomValue(index, Color32.Lerp);
        this.GetComponent<Rigidbody>().mass = star.Mass.RandomValue(index, Mathf.Lerp) * SpaceMath.SolMass * SpaceMath.ToEngineMass;
        this.Class = star.StarClass;
        this.Evolution = star.EvolutionState;
    }

    public void EvolveStar()
    {
        StarEvolution starEvolution = new StarEvolution(star);
        star = starEvolution.Evolve();
        float radius = star.Radius.Value;
        radius *= (SpaceMath.SolRadius / SpaceMath.Unit) * 2f;
        this.transform.localScale = new Vector3(radius, radius, radius);
        this.GetComponent<Renderer>().material.color = star.Color.Value;
        GetComponent<Rigidbody>().mass = star.Mass.Value * SpaceMath.SolMass * SpaceMath.ToEngineMass;
        this.Evolution = star.EvolutionState;
    }
}
