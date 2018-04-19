using UnityEngine;

namespace SpaceEngine
{
    [System.Serializable]
    public class PlanetData
    {
        public FRange Radius;
        public FRange Density;

        public static PlanetData A
        {
            get
            {
                return new PlanetData
                {
                    Radius = new FRange(500000f, 5000000f),
                    Density = new FRange(3000f, 4000f)
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
                    Density = new FRange(4000f, 6000f)
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
                    Density = new FRange(3200f, 4200f)
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
                    Density = new FRange(2000f, 3500f)
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
                    Density = new FRange(6200f, 12000f)
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
                    Density = new FRange(6200f, 12000f)
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
                    Density = new FRange(6200f, 12000f)
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
                    Density = new FRange(4000f, 5200f)
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
                    Density = new FRange(400f, 1700f)
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
                    Density = new FRange(600f, 1700f)
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
                    Density = new FRange(3000f, 4000f)
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
                    Density = new FRange(4000f, 5200f)
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
                    Density = new FRange(3500f, 6000f)
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
                    Density = new FRange(6000f, 7000f)
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
                    Density = new FRange(3500f, 6000f)
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
                    Density = new FRange(3500f, 6000f)
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
                    Density = new FRange(2500f, 5000f)
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
                    Density = new FRange(10000f, 100000f)
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
                    Density = new FRange(7500f, 13000f)
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
                    Density = new FRange(7500f, 13000f)
                };
            }
        }

        public PlanetData()
        {
            Radius = new FRange();
        }

        public static PlanetData GetData()
        {
            return null;
        }

        public static PlanetData GetData(PlanetClass planetClass)
        {
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
            return null;
        }

        private static PlanetData GetEcoZonePlanet()
        {
            return null;
        }

        private static PlanetData GetColdZonePlanet()
        {
            return null;
        }

        public bool IsEmpty()
        {
            if (Radius.Value == default(float) && Density.Value == default(float)) return true;
            return false;
        }
    }
}
