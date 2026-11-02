using System;
using TjuvOchPolis2;

namespace TjuvOchPolis2
{
    // Fängelset: 10 x 10, med väggar. Här rör sig alla tjuvar som blivit fångade av polisen
    public class Prison : Place
    {
        public Prison() : base("Prison", 10, 10, true)
        {
        }

        // Anropas hela tiden från Program. Släpper ut de tjuvar vars strafftid har gått ut
        public void Frigefångar(City stad, Random slump)
        {
            foreach (Person person in stad.Personer)
            {
                if (person is Thief && person.Plats == this)
                {
                    Thief thief = (Thief)person;

                    // Kollar om tjuven har suttit tillräckligt länge i fängelse
                    if (DateTime.Now >= thief.Frislapps)
                    {
                        thief.BytPlats(stad, slump);
                        stad.Händelser.Add(thief.Namn + " friges från fängelset.");
                    }
                }
            }
        }
    }
}