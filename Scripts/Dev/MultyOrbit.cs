using System.Collections;
using System.Collections.Generic;
using SpaceEngine;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MultyOrbit : MonoBehaviour
{
    public float SolForce;
    public float EarthForce;

    public float Distanse;

    public GameObject Sat;

    public GameObject Earth;

    void Start()
    {
        SolForce = 0;
        EarthForce = 0;

        //GetComponent<Rigidbody>().mass = SpaceMath.SolMass * SpaceMath.ToEngineMass;
        //Earth.GetComponent<Rigidbody>().mass = SpaceMath.EarthMass * SpaceMath.ToEngineMass;
        //Sat.GetComponent<Rigidbody>().mass = 7.34f * Mathf.Pow(10f, 22) * SpaceMath.ToEngineMass;

        //float jupMass = 1.8982f * Mathf.Pow(10, 27);

        //Debug.Log(70f * jupMass);

        //Debug.Log(4f * Mathf.Pow(10, -27));

        //Debug.Log((1.304f * Mathf.Pow(10f, 36f)) * SpaceMath.ToEngineMass );

        //Debug.Log(4.18f * Mathf.Pow(70000000f, 3) * 1700);
        //Debug.Log(4.18f * Mathf.Pow(10.5f * SpaceMath.EarthRadius, 3) * 1326);
        //Debug.Log(4.18f * Mathf.Pow(250000000, 3) * 1700);
        //Debug.Log(4.18f * Mathf.Pow(50000000f, 3) * 10000f);
        //Debug.Log(4.18f * Mathf.Pow(64400000f, 3) * 100000f);

        //Debug.Log((1.11f * Mathf.Pow(10, 28)) / SpaceMath.SolMass);

        //Debug.Log(0.079f *  SpaceMath.SolMass );

        //Debug.Log( (1.89 * Mathf.Pow(10, 27)) / SpaceMath.SolMass );

        //Debug.Log(Mathf.Pow( (70f * jupMass) / (4.18f * 100000f), 1f/3f));

        //6957000

        //64400000
        //140000000
        //147000000
        //255000000  70 Jup
        //275000000  min star

        //Debug.Log(Mathf.Pow((0.075f * SpaceMath.SolMass) / (4.18f * 1700f), 1f / 3f));

        //Debug.Log(64400000f / SpaceMath.SolRadius);

        //Debug.Log(0.01f * SpaceMath.SolRadius);

        //Debug.Log((70f * jupMass) / SpaceMath.SolMass);

        //Debug.Log(Mathf.Pow((0.079f * SpaceMath.SolMass) / (4.18f * 1700), 1f / 3f) / SpaceMath.SolRadius);

        //Debug.Log((0.079f * SpaceMath.SolMass) / (4.18f * Mathf.Pow(0.099f * SpaceMath.SolRadius, 3)));

        SolForce = SpaceMath.Gravity.GetGravityForce(gameObject, Sat);
        EarthForce = SpaceMath.Gravity.GetGravityForce(Earth, Sat);

        //Debug.Log(this.GetComponent<Rigidbody>().mass / Mathf.Pow( (Sat.transform.position - this.transform.position).magnitude, 2));

        //Debug.Log(Earth.GetComponent<Rigidbody>().mass / Mathf.Pow( (Sat.transform.position - Earth.transform.position).magnitude, 2));

        //Debug.Log(SpaceMath.SolRadius * 109.25f);
        //Debug.Log((SpaceMath.SolRadius * 109.25f) / SpaceMath.AU);
        //Debug.Log( 50f * SpaceMath.AU);

        //PlanetPosition planetPosition = new PlanetPosition();
        //planetPosition.GenerateSeed();
        //for (int i = 0; i < 10; i++)
        //{
        //    Debug.Log(planetPosition.PositionAt(i));
        //}

        SatellitePosition satellitePosition = new SatellitePosition();
        satellitePosition.GenerateSeed();
        for (int i = 0; i < 10; i++)
        {
            Debug.Log(satellitePosition.PositionAt(i));
        }

        //for (int i = 0; i < 10; i++)
        //{
        //    float d = 9.1f * Mathf.Pow(0.52f, i);
        //    //float d = 0.5f * Mathf.Pow(9.52f, i);
        //    Debug.Log((d + 4) / 10);
        //    //Debug.Log(((d + 4) / 10) * SpaceMath.AU);

        //    //Debug.Log(pos.NextPosition());
        //}

        //Debug.Log((4.18 * 46656) / (6 * Mathf.Pow(10, 16)));
    }

    void Update()
    {
        //Force = SpaceMath.GetGravityForce(gameObject, Sat);

        SolForce = SpaceMath.Gravity.GetGravityForce(gameObject, Sat);
        EarthForce = SpaceMath.Gravity.GetGravityForce(Earth, Sat);

        Distanse = (Sat.transform.position - Earth.transform.position).magnitude;
    }
}