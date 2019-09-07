using System;
using UnityEngine;

namespace SpaceEngine
{

    public class Range<T>
    {
        public T From;
        public T To;

        public T Value;

        public Range() { }

        public Range(T from, T to)
        {
            this.From = from;
            this.To = to;
            this.Value = default(T);
        }

        public T RandomValue(Func<T, T, float, T> Lerp)
        {
            float t = UnityEngine.Random.Range(0f, 1f);
            return RandomValue(t, Lerp);
        }

        public T RandomValue(float t, Func<T, T, float, T> Lerp)
        {
            this.Value = Lerp(From, To, t);
            return this.Value;
        }
    }

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