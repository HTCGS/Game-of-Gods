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
                    //white dwarf  // светимость в 10 раз
                    if (Random.Range(0f, 1f) < 0.5f)
                    {
                        evolvedStar.Radius.Value *= Random.Range(1.05f, 1.3f);
                        evolvedStar.EvolutionState = StarEvolutionState.SubGiant;
                    }
                    else
                    {

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
                    // white dwarf

                }
                else if (Star.Mass.Value <= 10)
                {
                    if(Star.StarClass.IsOneOf(StarClass.O, StarClass.B))
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
            else if(Star.EvolutionState == StarEvolutionState.Giant)
            {
                
            }
            else if(Star.EvolutionState == StarEvolutionState.SuperGiant)
            {

            }
            else if (Star.EvolutionState == StarEvolutionState.HyperGiant)
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
