using UnityEngine;

namespace SpaceEngine
{
    class StarEvolution
    {
        public StarData Star;

        public StarEvolution()
        {

        }

        public StarEvolution(StarData star)
        {
            this.Star = star;
        }

        public StarData Evolve()
        {
            StarData evolvedStar = Star.Copy();
            if (Star.EvolutionState == StarEvolutionState.Main)
            {
                if (Star.StarClass.IsOneOf(StarClass.B, StarClass.A, StarClass.F, StarClass.G, StarClass.K))
                {
                    evolvedStar.Radius.Value *= Random.Range(1.2f, 2f);
                    evolvedStar.EvolutionState = StarEvolutionState.SubGiant;
                }
                else if (Star.StarClass == StarClass.M)
                {
                    if (Star.Mass.Value < 0.4)
                    {
                        evolvedStar.Color.Value = new Color32(1, 1, 1, 1);
                        evolvedStar.EvolutionState = StarEvolutionState.Dwarf;
                    }
                    else
                    {
                        evolvedStar.Radius.Value *= Random.Range(1.2f, 2f);
                        evolvedStar.EvolutionState = StarEvolutionState.SubGiant;
                    }
                }
                else
                {
                    if (Star.Mass.Value > 10 && Star.Mass.Value < 70)
                    {
                        evolvedStar.Radius.Value = Random.Range(30f, 500f);
                        evolvedStar.EvolutionState = StarEvolutionState.SuperGiant;
                    }
                    else
                    {
                        evolvedStar.Radius.Value = Random.Range(500f, 1500f);
                        evolvedStar.EvolutionState = StarEvolutionState.HyperGiant;
                    }
                }
            }
            else if (Star.EvolutionState == StarEvolutionState.SubGiant)
            {
                if (Star.Mass.Value <= 0.5)
                {
                    evolvedStar.Mass.Value *= Random.Range(0.8f, 0.9f);
                    evolvedStar.Radius.Value = Random.Range(0.8f, 2.2f) / Random.Range(70, 110);
                    evolvedStar.Color.Value = new Color32(1, 1, 1, 1);
                    evolvedStar.EvolutionState = StarEvolutionState.Dwarf;
                }
                else if (Star.Mass.Value <= 10)
                {
                    if (Star.StarClass.IsOneOf(StarClass.O, StarClass.B))
                    {
                        evolvedStar.Radius.Value = Random.Range(5f, 10f);
                        evolvedStar.EvolutionState = StarEvolutionState.Giant;
                    }
                    else if (Star.StarClass.IsOneOf(StarClass.F, StarClass.G))
                    {
                        evolvedStar.Radius.Value *= Random.Range(10f, 100f);
                        evolvedStar.EvolutionState = StarEvolutionState.Giant;
                    }
                    else
                    {
                        evolvedStar.Radius.Value *= Random.Range(10f, 100f);
                        evolvedStar.EvolutionState = StarEvolutionState.Giant;
                    }
                }
                else
                {
                    if (Star.Mass.Value > 10 && Star.Mass.Value < 70)
                    {
                        evolvedStar.Radius.Value = Random.Range(30f, 500f);
                        evolvedStar.EvolutionState = StarEvolutionState.SuperGiant;
                    }
                    else
                    {
                        evolvedStar.Radius.Value = Random.Range(500f, 1500f);
                        evolvedStar.EvolutionState = StarEvolutionState.HyperGiant;
                    }
                }
            }
            else if (Star.EvolutionState == StarEvolutionState.Giant
                || Star.EvolutionState == StarEvolutionState.SuperGiant
                || Star.EvolutionState == StarEvolutionState.HyperGiant)
            {
                evolvedStar.Mass.Value *= Random.Range(0.5f, 0.8f);

                if (evolvedStar.Mass.Value < 1.44)
                {
                    evolvedStar.Radius.Value = Random.Range(0.8f, 2.2f) / Random.Range(70, 110);
                    evolvedStar.Color.Value = new Color32(1, 1, 1, 1);
                    evolvedStar.EvolutionState = StarEvolutionState.Dwarf;
                }
                else if (evolvedStar.Mass.Value < 3)
                {
                    evolvedStar.Radius.Value = (Random.Range(10f, 20f) * 1000f) / SpaceMath.SolRadius;
                    evolvedStar.Color.Value = new Color32(0, 255, 255, 1);
                    evolvedStar.EvolutionState = StarEvolutionState.Neutron;
                }
                else
                {
                    evolvedStar.Radius.Value = SpaceMath.GravitationalRadius(evolvedStar.Mass.Value * SpaceMath.SolMass) / SpaceMath.SolRadius;
                    evolvedStar.Color.Value = new Color32(0, 0, 0, 1);
                    evolvedStar.EvolutionState = StarEvolutionState.BlackHole;
                }
            }
            else if (Star.EvolutionState == StarEvolutionState.Dwarf)
            {

            }
            return evolvedStar;
        }

        public StarData Evolve(StarData star)
        {
            this.Star = star;
            return Evolve();
        }
    }
}
