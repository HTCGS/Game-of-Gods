using UnityEngine;

namespace SpaceEngine
{
    [System.Serializable]
    public class PlanetData
    {
        public FRange Radius;
        public FRange Density;
        public PlanetClass PlanetClass;

        public static PlanetData A
        {
            get
            {
                return new PlanetData
                {
                    Radius = new FRange(500000f, 5000000f),
                        Density = new FRange(3000f, 4000f),
                        PlanetClass = PlanetClass.A
                };
            }
        }

        public static PlanetData B
        {
            get
            {
                return new PlanetData
                {
                    Radius = new FRange(500000f, 5000000f),
                        Density = new FRange(4000f, 6000f),
                        PlanetClass = PlanetClass.B
                };
            }
        }

        public static PlanetData C
        {
            get
            {
                return new PlanetData
                {
                    Radius = new FRange(500000f, 5000000f),
                        Density = new FRange(3200f, 4200f),
                        PlanetClass = PlanetClass.C
                };
            }
        }

        public static PlanetData D
        {
            get
            {
                return new PlanetData
                {
                    Radius = new FRange(50000f, 500000f),
                        Density = new FRange(2000f, 3500f),
                        PlanetClass = PlanetClass.D
                };
            }
        }

        public static PlanetData E
        {
            get
            {
                return new PlanetData
                {
                    Radius = new FRange(5000000f, 15000000f),
                        Density = new FRange(6200f, 12000f),
                        PlanetClass = PlanetClass.E
                };
            }
        }

        public static PlanetData F
        {
            get
            {
                return new PlanetData
                {
                    Radius = new FRange(5000000f, 15000000f),
                        Density = new FRange(6200f, 12000f),
                        PlanetClass = PlanetClass.F
                };
            }
        }

        public static PlanetData G
        {
            get
            {
                return new PlanetData
                {
                    Radius = new FRange(5000000f, 15000000f),
                        Density = new FRange(6200f, 12000f),
                        PlanetClass = PlanetClass.G
                };
            }
        }

        public static PlanetData H
        {
            get
            {
                return new PlanetData
                {
                    Radius = new FRange(4000000f, 7500000f),
                        Density = new FRange(4000f, 5200f),
                        PlanetClass = PlanetClass.H
                };
            }
        }

        public static PlanetData I
        {
            get
            {
                return new PlanetData
                {
                    Radius = new FRange(15000000f, 50000000f),
                        Density = new FRange(400f, 1700f),
                        PlanetClass = PlanetClass.I
                };
            }
        }

        public static PlanetData J
        {
            get
            {
                return new PlanetData
                {
                    Radius = new FRange(25000000f, 70000000f),
                        Density = new FRange(600f, 1700f),
                        PlanetClass = PlanetClass.J
                };
            }
        }

        public static PlanetData K
        {
            get
            {
                return new PlanetData
                {
                    Radius = new FRange(2500000f, 5000000f),
                        Density = new FRange(3000f, 4000f),
                        PlanetClass = PlanetClass.K
                };
            }
        }

        public static PlanetData L
        {
            get
            {
                return new PlanetData
                {
                    Radius = new FRange(5000000f, 7500000f),
                        Density = new FRange(4000f, 5200f),
                        PlanetClass = PlanetClass.L
                };
            }
        }

        public static PlanetData M
        {
            get
            {
                return new PlanetData
                {
                    Radius = new FRange(5000000f, 7500000f),
                        Density = new FRange(3500f, 6000f),
                        PlanetClass = PlanetClass.M
                };
            }
        }

        public static PlanetData N
        {
            get
            {
                return new PlanetData
                {
                    Radius = new FRange(5000000f, 7500000f),
                        Density = new FRange(6000f, 7000f),
                        PlanetClass = PlanetClass.N
                };
            }
        }

        public static PlanetData O
        {
            get
            {
                return new PlanetData
                {
                    Radius = new FRange(5000000f, 7500000f),
                        Density = new FRange(3500f, 6000f),
                        PlanetClass = PlanetClass.O
                };
            }
        }

        public static PlanetData P
        {
            get
            {
                return new PlanetData
                {
                    Radius = new FRange(5000000f, 7500000f),
                        Density = new FRange(3500f, 6000f),
                        PlanetClass = PlanetClass.P
                };
            }
        }

        public static PlanetData Q
        {
            get
            {
                return new PlanetData
                {
                    Radius = new FRange(2000000f, 7500000f),
                        Density = new FRange(2500f, 5000f),
                        PlanetClass = PlanetClass.Q
                };
            }
        }

        public static PlanetData S
        {
            get
            {
                return new PlanetData
                {
                    Radius = new FRange(50000000f, 64400000f),
                        Density = new FRange(10000f, 100000f),
                        PlanetClass = PlanetClass.S
                };
            }
        }

        public static PlanetData X
        {
            get
            {
                return new PlanetData
                {
                    Radius = new FRange(500000f, 5000000f),
                        Density = new FRange(7500f, 13000f),
                        PlanetClass = PlanetClass.X
                };
            }
        }

        public static PlanetData Y
        {
            get
            {
                return new PlanetData
                {
                    Radius = new FRange(5000000f, 7500000f),
                        Density = new FRange(7500f, 13000f),
                        PlanetClass = PlanetClass.Y
                };
            }
        }

        public PlanetData()
        {
            Radius = new FRange();
        }

        public static PlanetData GetData()
        {
            PlanetClass planetClass = (PlanetClass) Random.Range(1, 21);
            return GetData(planetClass);
        }

        public static PlanetData GetData(PlanetClass planetClass)
        {
            switch (planetClass)
            {
                case PlanetClass.Random:
                    return GetData();
                case PlanetClass.A:
                    return PlanetData.A;
                case PlanetClass.B:
                    return PlanetData.B;
                case PlanetClass.C:
                    return PlanetData.C;
                case PlanetClass.D:
                    return PlanetData.D;
                case PlanetClass.E:
                    return PlanetData.E;
                case PlanetClass.F:
                    return PlanetData.F;
                case PlanetClass.G:
                    return PlanetData.G;
                case PlanetClass.H:
                    return PlanetData.H;
                case PlanetClass.I:
                    return PlanetData.I;
                case PlanetClass.J:
                    return PlanetData.J;
                case PlanetClass.K:
                    return PlanetData.K;
                case PlanetClass.L:
                    return PlanetData.L;
                case PlanetClass.M:
                    return PlanetData.M;
                case PlanetClass.N:
                    return PlanetData.N;
                case PlanetClass.O:
                    return PlanetData.O;
                case PlanetClass.P:
                    return PlanetData.P;
                case PlanetClass.Q:
                    return PlanetData.Q;
                case PlanetClass.S:
                    return PlanetData.S;
                case PlanetClass.X:
                    return PlanetData.X;
                case PlanetClass.Y:
                    return PlanetData.Y;
                default:
                    break;
            }
            return null;
        }

        public static PlanetData GetData(Zone zone)
        {
            if (zone == Zone.Hot) return GetHotZonePlanet();
            if (zone == Zone.Eco) return GetEcoZonePlanet();
            return GetColdZonePlanet();
        }

        private static PlanetData GetHotZonePlanet()
        {
            int planetClass = Random.Range(0, 7);
            if (planetClass == 0) return GetData(PlanetClass.A);
            if (planetClass == 1) return GetData(PlanetClass.B);
            if (planetClass == 2) return GetData(PlanetClass.C);
            if (planetClass == 3) return GetData(PlanetClass.D);
            if (planetClass == 4) return GetData(PlanetClass.Q);
            if (planetClass == 5) return GetData(PlanetClass.X);
            if (planetClass == 6) return GetData(PlanetClass.Y);
            return null;
        }

        private static PlanetData GetEcoZonePlanet()
        {
            int planetClass = Random.Range(0, 14);
            if (planetClass == 0) return GetData(PlanetClass.A);
            if (planetClass == 1) return GetData(PlanetClass.C);
            if (planetClass == 2) return GetData(PlanetClass.D);
            if (planetClass == 3) return GetData(PlanetClass.E);
            if (planetClass == 4) return GetData(PlanetClass.F);
            if (planetClass == 5) return GetData(PlanetClass.G);
            if (planetClass == 6) return GetData(PlanetClass.H);
            if (planetClass == 7) return GetData(PlanetClass.K);
            if (planetClass == 8) return GetData(PlanetClass.L);
            if (planetClass == 9) return GetData(PlanetClass.M);
            if (planetClass == 10) return GetData(PlanetClass.N);
            if (planetClass == 11) return GetData(PlanetClass.O);
            if (planetClass == 12) return GetData(PlanetClass.P);
            if (planetClass == 13) return GetData(PlanetClass.Q);
            return null;
        }

        private static PlanetData GetColdZonePlanet()
        {
            int planetClass = Random.Range(0, 8);
            if (planetClass == 0) return GetData(PlanetClass.A);
            if (planetClass == 1) return GetData(PlanetClass.C);
            if (planetClass == 2) return GetData(PlanetClass.D);
            if (planetClass == 3) return GetData(PlanetClass.I);
            if (planetClass == 4) return GetData(PlanetClass.J);
            if (planetClass == 5) return GetData(PlanetClass.P);
            if (planetClass == 6) return GetData(PlanetClass.Q);
            if (planetClass == 7) return GetData(PlanetClass.S);
            return null;
        }

        public bool IsEmpty()
        {
            if (Radius.Value == default(float) && Density.Value == default(float)) return true;
            return false;
        }
    }
}