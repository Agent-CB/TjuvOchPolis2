using System;
using System.Collections.Generic;

namespace TjuvOchPolis2
{

    public class City : Place
    {
        //  Det City håller reda på
        public List<Person> Personer { get; set; } 
        public List<string> Händelser { get; set; }
        public Prison Prison { get; set; }
        public PoorHouse PoorHouse { get; set; }
        public int AntalRånade { get; set; }
        public int AntalGripanden { get; set; }

        private List<Citizen> rånadeMedborgare = new List<Citizen>();

        public City() : base("City", 100, 25, false)
        {
            Personer = new List<Person>();
            Händelser = new List<string>();
            Prison = new Prison();
            PoorHouse = new PoorHouse();
        }

        public void LäggTillCitizen(Random slump)
        {
            Citizen citizen = new Citizen("Medborgare " + (Personer.Count + 1), this, slump);
            Personer.Add(citizen);
            Händelser.Add(citizen.Namn + " flyttar in i staden.");
        }

        public void LäggTillThief(Random slump)
        {
            Thief thief = new Thief("Tjuv " + (Personer.Count + 1), this, slump);
            Personer.Add(thief);
            Händelser.Add(thief.Namn + " flyttar in i staden.");
        }

        public void LäggTillPolice(Random slump)
        {
            Police police = new Police("Polis " + (Personer.Count + 1), this, slump);
            Personer.Add(police);
            Händelser.Add(police.Namn + " flyttar in i staden.");
        }

        public void FlyttaAlla(Random slump)
        {
            foreach (Person person in Personer)
            {
                person.Flytta(slump);
            }
        }

        public void FlyttaTillPlats(Person person, Place plats, Random slump)
        {
            person.Plats = plats;
            person.X = slump.Next(plats.Bredd);
            person.Y = slump.Next(plats.Höjd);
        }

        public int RäknaTyp(char symbol)
        {
            int antal = 0;

            foreach (Person person in Personer)
            {
                if (person.Plats == this && person.Symbol == symbol)
                {
                    antal++;
                }
            }

            return antal;
        }

        public int RäknaPåPlats(Place plats)
        {
            int antal = 0;

            foreach (Person person in Personer)
            {
                if (person.Plats == plats)
                {
                    antal++;
                }
            }

            return antal;
        }

        public void KontrolleraMöten(Random slump)
        {
            
            foreach (Person första in Personer)
            {
                foreach (Person andra in Personer)
                {
                    if (första.Plats != this || andra.Plats != this)
                    {
                        continue;
                    }

                    if (första.X != andra.X || första.Y != andra.Y)
                    {
                        continue;
                    }
  
                    if (första is Police && andra is Thief)
                    {
                        Police police = (Police)första;
                        Thief thief = (Thief)andra;
                        Gripa(police, thief, slump);
                    }

                    if (första is Thief && andra is Citizen)
                    {
                        Thief thief = (Thief)första;
                        Citizen citizen = (Citizen)andra;
                        Råna(thief, citizen, slump);
                    }

                }
            }
        }

        // Polisen griper tjuven, men bara om tjuven har stöldgods.
        private void Gripa(Police police, Thief thief, Random slump)
        {
            int antal = thief.Inventory.Count;

            if (antal == 0)
            {
                return;
            }

            foreach (Item sak in thief.Inventory)
            {
                police.Inventory.Add(sak);
            }

            thief.Inventory.Clear();

            thief.Frisläpps = DateTime.Now.AddSeconds(antal * 10);
            FlyttaTillPlats(thief, Prison, slump);

            AntalGripanden++;
            Händelser.Add(police.Namn + " griper " + thief.Namn + ". Straff: " + (antal * 10) + " sekunder.");
        }

        private void Råna(Thief thief, Citizen citizen, Random slump)
        {
            if (citizen.Inventory.Count == 0)
            {
                return;
            }

            int index = slump.Next(citizen.Inventory.Count);
            Item sak = citizen.Inventory[index];
            citizen.Inventory.RemoveAt(index);
            thief.Inventory.Add(sak);

            if (!rånadeMedborgare.Contains(citizen))
            {
                rånadeMedborgare.Add(citizen);
                AntalRånade++;
            }

            Händelser.Add(thief.Namn + " rånar " + citizen.Namn + " och tar " + sak.Namn + ".");

            if (citizen.Inventory.Count == 0)
            {
                FlyttaTillPlats(citizen, PoorHouse, slump);
                Händelser.Add(citizen.Namn + " flyttar till fattighuset.");
            }
        }
    }
}