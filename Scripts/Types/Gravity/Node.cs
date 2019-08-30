using UnityEngine;

namespace SpaceEngine.Gravity
{
    public class Node
    {
        public float Mass;
        public Vector3 CenterOfMass;

        public Node[] Childrens;
    }
}