using System.Collections.Generic;
using UnityEngine;

namespace SpaceEngine
{
    [System.Serializable]
    public class SatellitePosition : TitiusBodeLaw
    {
        public GameObject Star;

        public SatellitePosition()
        {
        }

        public SatellitePosition(GameObject parent) : base(parent)
        {
            Star = parent.transform.parent.gameObject;
        }

        public SatellitePosition(Rigidbody parent) : base(parent)
        {
            Star = parent.transform.parent.gameObject;
        }

        public override void GenerateSeed()
        {
            GenerateSeed(Random.Range(0.1f, 1f), Random.Range(1f, 2f));
        }

        protected override int MaxPositionIndex()
        {
            int index = -1;
            if (Parent.transform.parent == null) return index;
            float starMass = Star.GetComponent<Rigidbody>().mass;
            float planetMass = Parent.GetComponent<Rigidbody>().mass;
            float starGravity = 0;
            float planetGravity = 0;
            do
            {
                index++;
                float orbitRadius = PositionAt(index);
                orbitRadius *= 10000000f;
                orbitRadius *= SpaceMath.Unit;
                float toStar = (Star.transform.position - Parent.transform.position).magnitude;
                toStar += orbitRadius;
                toStar *= SpaceMath.Unit;
                starGravity = SpaceMath.GetGravityForce(starMass, 1f, toStar);
                planetGravity = SpaceMath.GetGravityForce(planetMass, 1f, orbitRadius);
            } while (planetGravity / starGravity >= 1.5f);
            return index;
        }
    }
}
