using System.Collections.Generic;
using UnityEngine;

namespace SpaceEngine
{
    [System.Serializable]
    public class SatellitePosition : TitiusBodeLaw
    {
        public SatellitePosition()
        {
        }

        public SatellitePosition(GameObject parent) : base(parent)
        {
        }

        public SatellitePosition(Rigidbody parent) : base(parent)
        {
        }

        public override void GenerateSeed()
        {
            GenerateSeed(Random.Range(0.1f, 1f), Random.Range(1f, 2f));
        }

        protected override int MaxPositionIndex()
        {
            int index = -1;
            if (Parent.transform.parent == null) return index;
            float starMass = Parent.transform.parent.GetComponent<Rigidbody>().mass;
            float planetMass = Parent.GetComponent<Rigidbody>().mass;
            float starGravity = 0;
            float planetGravity = 0;
            do
            {
                index++;
                float orbitRadius = PositionAt(index);
                orbitRadius *= 1000000f;
                starGravity = SpaceMath.GetGravityForce(starMass, 1f, orbitRadius);
                planetGravity = SpaceMath.GetGravityForce(planetMass, 1f, orbitRadius);
            } while (planetGravity / starGravity >= 1.5f);
            return index;
        }
    }
}
