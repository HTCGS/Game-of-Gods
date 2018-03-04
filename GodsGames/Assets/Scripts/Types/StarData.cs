﻿using UnityEngine;

namespace SpaceEngine
{
    public class StarData
    {
        public Range<float> Mass;
        public Range<float> Radius;
        public Range<Color32> Color;
        public float Luminosity;
        public StarClass StarClass;
        public StarEvolutionState EvolutionState;

        public static StarData O
        {
            get
            {
                return new StarData
                {
                    Mass = new Range<float>(25f, 58f),
                    Radius = new Range<float>(9f, 16f),
                    Color = new Range<Color32>(new Color32(62, 108, 255, 255), new Color32(61, 107, 255, 255)),
                    StarClass = StarClass.O
                };
            }
        }

        public static StarData B
        {
            get
            {
                return new StarData
                {
                    Mass = new Range<float>(3.4f, 16f),
                    Radius = new Range<float>(3.7f, 5.7f),
                    Color = new Range<Color32>(new Color32(87, 133, 255, 255), new Color32(68, 114, 255, 255)),
                    StarClass = StarClass.B
                };
            }
        }

        public static StarData A
        {
            get
            {
                return new StarData
                {
                    Mass = new Range<float>(1.9f, 2.6f),
                    Radius = new Range<float>(1.8f, 2.3f),
                    Color = new Range<Color32>(new Color32(156, 189, 255, 255), new Color32(124, 165, 255, 255)),
                    StarClass = StarClass.A
                };
            }
        }

        public static StarData F
        {
            get
            {
                return new StarData
                {
                    Mass = new Range<float>(1.2f, 1.6f),
                    Radius = new Range<float>(1.2f, 1.5f),
                    Color = new Range<Color32>(new Color32(212, 228, 255, 255), new Color32(177, 204, 255, 255)),
                    StarClass = StarClass.F
                };
            }
        }

        public static StarData G
        {
            get
            {
                return new StarData
                {
                    Mass = new Range<float>(0.86f, 1.08f),
                    Radius = new Range<float>(0.98f, 1.05f),
                    Color = new Range<Color32>(new Color32(255, 246, 233, 255), new Color32(237, 244, 255, 255)),
                    StarClass = StarClass.G
                };
            }
        }

        public static StarData K
        {
            get
            {
                return new StarData
                {
                    Mass = new Range<float>(0.58f, 0.83f),
                    Radius = new Range<float>(0.75f, 0.89f),
                    Color = new Range<Color32>(new Color32(255, 203, 145, 255), new Color32(255, 233, 203, 255)),
                    StarClass = StarClass.K
                };
            }
        }

        public static StarData M
        {
            get
            {
                return new StarData
                {
                    Mass = new Range<float>(0.08f, 0.47f),
                    Radius = new Range<float>(0.10f, 0.64f),
                    Color = new Range<Color32>(new Color32(208, 0, 0, 255), new Color32(255, 174, 98, 255)),
                    StarClass = StarClass.M
                };
            }
        }

        public StarData()
        {
            this.Mass = new Range<float>();
            this.Radius = new Range<float>();
            this.Color = new Range<Color32>();
            this.EvolutionState = StarEvolutionState.Main;
        }

        public static StarData GetStarData()
        {
            float rnd = Random.Range(0.00001f, 100f);

            if (rnd >= 0.00001f && rnd <= 0.00003f) return StarData.O;
            if (rnd > 0.00003f && rnd <= 0.12f) return StarData.B;
            if (rnd > 0.12f && rnd <= 0.72f) return StarData.A;
            if (rnd > 0.72f && rnd <= 3.72f) return StarData.F;
            if (rnd > 3.72f && rnd <= 11.32f) return StarData.G;
            if (rnd > 11.32f && rnd <= 23.45f) return StarData.K;
            if (rnd > 23.45f && rnd <= 100f) return StarData.M;
            return null;
        }

        public static StarData GetStarData(StarClass starClass)
        {
            switch (starClass)
            {
                case StarClass.Random:
                    return GetStarData();
                case StarClass.O:
                    return StarData.O;
                case StarClass.B:
                    return StarData.B;
                case StarClass.A:
                    return StarData.A;
                case StarClass.F:
                    return StarData.F;
                case StarClass.G:
                    return StarData.G;
                case StarClass.K:
                    return StarData.K;
                case StarClass.M:
                    return StarData.M;
                default:
                    break;
            }
            return null;
        }

        public StarData Copy()
        {
            StarData copyed = new StarData
            {
                Mass = new Range<float>(this.Mass.From, this.Mass.To),
                Radius = new Range<float>(this.Radius.From, this.Radius.To),
                Color = new Range<Color32>(this.Color.From, this.Color.To),
                StarClass = this.StarClass,
                EvolutionState = this.EvolutionState
            };
            copyed.Mass.Value = this.Mass.Value;
            copyed.Radius.Value = this.Radius.Value;
            return copyed;
        }

        public void Mutate()
        {
            if(Random.Range(0, 100) < 5)
            {
                float mult = Random.Range(10f, 100f);
                float num = Random.Range(0.1f, 2.5f);
                this.Mass.Value = mult * num;
                this.EvolutionState = StarEvolutionState.SubGiant;
            }
            else
            {
                this.Mass.Value *= Random.Range(0.75f, 1.50f);
                this.Radius.Value *= Random.Range(0.5f, 2f);
            }
        }
    }
}
