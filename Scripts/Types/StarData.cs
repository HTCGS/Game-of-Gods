using UnityEngine;

namespace SpaceEngine
{
    [System.Serializable]
    public class StarData
    {
        public FRange Mass;
        public FRange Radius;
        public CRange Color;
        public FRange Luminosity;
        public StarClass StarClass;
        public StarEvolutionState EvolutionState;

        public static StarData O
        {
            get
            {
                return new StarData
                {
                    Mass = new FRange(25f, 58f),
                    Radius = new FRange(9f, 16f),
                    Luminosity = new FRange(140000f, 800000f),
                    Color = new CRange(new Color32(62, 108, 255, 255), new Color32(61, 107, 255, 255)),
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
                    Mass = new FRange(3.4f, 16f),
                    Radius = new FRange(3.7f, 5.7f),
                    Luminosity = new FRange(130f, 16000f),
                    Color = new CRange(new Color32(87, 133, 255, 255), new Color32(68, 114, 255, 255)),
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
                    Mass = new FRange(1.9f, 2.6f),
                    Radius = new FRange(1.8f, 2.3f),
                    Luminosity = new FRange(11f, 63f),
                    Color = new CRange(new Color32(156, 189, 255, 255), new Color32(124, 165, 255, 255)),
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
                    Mass = new FRange(1.2f, 1.6f),
                    Radius = new FRange(1.2f, 1.5f),
                    Luminosity = new FRange(2.5f, 9f),
                    Color = new CRange(new Color32(212, 228, 255, 255), new Color32(177, 204, 255, 255)),
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
                    Mass = new FRange(0.86f, 1.08f),
                    Radius = new FRange(0.98f, 1.05f),
                    Luminosity = new FRange(0.44f, 1.45f),
                    Color = new CRange(new Color32(255, 246, 233, 255), new Color32(237, 244, 255, 255)),
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
                    Mass = new FRange(0.58f, 0.83f),
                    Radius = new FRange(0.75f, 0.89f),
                    Luminosity = new FRange(0.12f, 0.36f),
                    Color = new CRange(new Color32(255, 203, 145, 255), new Color32(255, 233, 203, 255)),
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
                    Mass = new FRange(0.08f, 0.47f),
                    Radius = new FRange(0.10f, 0.64f),
                    Luminosity = new FRange(0.0001f, 0.075f),
                    Color = new CRange(new Color32(208, 0, 0, 255), new Color32(255, 174, 98, 255)),
                    StarClass = StarClass.M
                };
            }
        }

        public StarData()
        {
            this.Mass = new FRange();
            this.Radius = new FRange();
            this.Color = new CRange();
            this.Luminosity = new FRange();
            this.EvolutionState = StarEvolutionState.Main;
        }

        public static StarData GetData()
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

        public static StarData GetData(StarClass starClass)
        {
            switch (starClass)
            {
                case StarClass.Random:
                    return GetData();
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

        public void Mutate()
        {
            if (Random.Range(0, 100) < 5)
            {
                float mult = Random.Range(10f, 100f);
                float num = Random.Range(0.1f, 2.5f);
                this.Mass.Value = mult * num;
            }
            else
            {
                this.Mass.Value *= Random.Range(0.75f, 1.50f);
                this.Radius.Value *= Random.Range(0.5f, 2f);
            }
        }

        public bool IsEmpty()
        {
            if (Mass.Value == default(float) && Radius.Value == default(float)) return true;
            return false;
        }

        public StarData Copy()
        {
            StarData copy = new StarData
            {
                Mass = new FRange(this.Mass.From, this.Mass.To),
                Radius = new FRange(this.Radius.From, this.Radius.To),
                Color = new CRange(this.Color.From, this.Color.To),
                Luminosity = new FRange(this.Luminosity.From, this.Luminosity.To),
                StarClass = this.StarClass,
                EvolutionState = this.EvolutionState
            };
            copy.Mass.Value = this.Mass.Value;
            copy.Radius.Value = this.Radius.Value;
            copy.Color.Value = this.Color.Value;
            copy.Luminosity.Value = this.Luminosity.Value;
            return copy;
        }
    }
}
