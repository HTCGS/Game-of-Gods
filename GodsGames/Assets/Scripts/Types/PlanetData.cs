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
                    Density = new FRange()
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
                    Density = new FRange()
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
                    Density = new FRange()
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
                    Density = new FRange()
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
                    Density = new FRange()
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
                    Density = new FRange()
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
                    Density = new FRange()
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
                    Density = new FRange()
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
                    Density = new FRange()
                };
            }
        }

        public static PlanetData J
        {
            get
            {
                return new PlanetData
                {
                    Radius = new FRange(25000000f, 250000000f),
                    Density = new FRange()
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
                    Density = new FRange()
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
                    Density = new FRange()
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
                    Density = new FRange()
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
                    Density = new FRange()
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
                    Density = new FRange()
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
                    Density = new FRange()
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
                    Density = new FRange()
                };
            }
        }

        public static PlanetData S
        {
            get
            {
                return new PlanetData
                {
                    Radius = new FRange(250000000f, 25000000000f),
                    Density = new FRange()
                };
            }
        }

        public static PlanetData U
        {
            get
            {
                return new PlanetData
                {
                    Radius = new FRange(25000000000f, 60000000000f),
                    Density = new FRange()
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
                    Density = new FRange()
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
                    Density = new FRange()
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
    }
}
