using System.Collections.Generic;
using UnityEngine;

namespace SpaceEngine
{
    [System.Serializable]
    public class PlanetPosition : TitiusBodeLaw
    {
        public PlanetPosition()
        {
        }

        public PlanetPosition(GameObject parent) : base(parent)
        {
        }

        public PlanetPosition(Rigidbody parent) : base(parent)
        {
        }

        public override void GenerateSeed()
        {
            GenerateSeed(Random.Range(0.001f, 10f), Random.Range(1f, 10f));
        }
    }
}
