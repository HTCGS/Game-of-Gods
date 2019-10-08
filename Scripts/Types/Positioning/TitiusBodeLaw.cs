using System.Collections.Generic;
using UnityEngine;

namespace SpaceEngine
{
    [System.Serializable]
    public class TitiusBodeLaw
    {
        public GameObject Parent;

        [SerializeField]
        protected float R;
        [SerializeField]
        protected float C;

        [SerializeField]
        protected int index;
        [SerializeField]
        protected int maxIndex;

        public TitiusBodeLaw() { }

        public TitiusBodeLaw(GameObject parent) : this()
        {
            this.Parent = parent;
        }

        public TitiusBodeLaw(Rigidbody parent) : this()
        {
            this.Parent = parent.gameObject;
        }

        public virtual void SetParent(GameObject parent)
        {
            this.Parent = parent;
        }

        public virtual float NextPosition()
        {
            float position = 0;
            if (index < maxIndex)
            {
                position = PositionAt(index);
                index++;
            }
            return position;
        }

        public virtual float PositionAt(int index)
        {
            return (R * Mathf.Pow(C, index)) / 10f;
        }

        public virtual List<float> Positions()
        {
            List<float> positions = new List<float>();
            for (int i = 0; i < maxIndex; i++)
            {
                index = i;
                positions.Add(PositionAt(index));
            }
            Clear();
            return positions;
        }

        public virtual void Clear()
        {
            index = 0;
        }

        public virtual void GenerateSeed()
        {
            GenerateSeed(3, 2);
        }

        public virtual void GenerateSeed(float r, float c)
        {
            this.R = r;
            this.C = c;
            this.index = 0;
            if (Parent != null) this.maxIndex = MaxPositionIndex();
            else this.maxIndex = 0;
        }

        protected virtual int MaxPositionIndex()
        {
            int index = -1;
            float parentMass = Parent.GetComponent<Rigidbody>().mass;
            float lowGravity = 4.77f * Mathf.Pow(10, -30);
            float gravityForce = 0;
            do
            {
                index++;
                float orbitRadius = PositionAt(index);
                orbitRadius *= SpaceMath.AU;
                gravityForce = SpaceMath.Gravity.GetGravityForce(parentMass, 1f, orbitRadius);
            } while (gravityForce > lowGravity);
            return index;
        }
    }
}