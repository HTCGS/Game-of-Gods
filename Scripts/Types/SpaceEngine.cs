using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceEngine
{
    public enum Rotation { Parent, Right, Left, Up, Down, Forward, Backward }

    public enum StarClass { Random, O, B, A, F, G, K, M }

    public enum StarEvolutionState { Random, Neutron, Dwarf, Main, SubGiant, Giant, SuperGiant, HyperGiant, BlackHole }

    public enum PlanetClass { Random, A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, S, X, Y }

    public enum Zone { Hot, Eco, Cold}

    [System.Serializable]
    public class FRange : Range<float>
    {
        public FRange() { }

        public FRange(float from, float to) : base(from, to) { }
    }
    [System.Serializable]
    public class CRange : Range<Color>
    {
        public CRange() { }

        public CRange(Color from, Color to) : base(from, to) { }
    }
}