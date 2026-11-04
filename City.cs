using System;
using System.Collections.Generic;

namespace TjuvOchPolis2
{
    public class City : Place
    {
        // Det City håller reda på 
        public List<Person> Personer { get; set; }
        public List<string> Händelser { get; set; }
        public Prison Prison { get; set; }
        public PoorHouse PoorHouse { get; set; }
        public int AntalRånade { get; set; }
        public int AntalGripanden { get; set; }

        private List<Citizen> rånadeCitizens = new List<Citizen>();
        private List<string> ledigaNamn = new List<string>
        {
            "Anders", "Erik", "Johan", "Anna", "Maria",
            "Emma", "Lucas", "Oscar", "Sara", "Lisa",
            "Anton", "William", "Oliver", "Elsa", "Ella",
            "Viktor", "Filip", "Maja", "Alma", "Hugo",
            "Nils", "Axel", "Klara", "Sofia", "David",
            "Liam", "Elias", "Ebba", "Wilma", "Simon",
            "Agnes", "Emil", "Felix", "Julia", "Olivia",
            "Theo", "Isak", "Linnea", "Elin", "Arvid"
        };

        private string HämtaNamn(Random slump)
        {
            int index = slump.Next(ledigaNamn.Count);
            return ledigaNamn[index];
        }

        public City() : base("City", 100, 25, false)
        {
            Personer = new List<Person>();
            Händelser = new List<string>();
            Prison = new Prison();
            PoorHouse = new PoorHouse();
        }

        // Lägga till personer
        public void LäggTillCitizen(Random slump)
        {
            Citizen citizen = new Citizen("Medborgaren " + HämtaNamn(slump), this, slump);
            Personer.Add(citizen);
            Händelser.Add(citizen.Namn + " flyttar in i staden.");
        }

        public void LäggTillThief(Random slump)
        {
            Thief thief = new Thief("Tjuven " + HämtaNamn(slump), this, slump);
            Personer.Add(thief);
            Händelser.Add(thief.Namn + " flyttar in i staden.");
        }

        public void LäggTillPolice(Random slump)
        {
            Police police = new Police("Polisen " + HämtaNamn(slump), this, slump);
            Personer.Add(police);
            Händelser.Add(police.Namn + " flyttar in i staden.");
        }

        // Gör så att alla personer flyttar ett steg
        public void FlyttaAlla(Random slump)
        {
            foreach (Person person in Personer)
            {
                person.Flytta(slump);
            }
        }

        // Räkna personer för screen.cs 

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

        // Antal personer på en viss yta
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

        // För möten
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

                    // Polis möter tjuv
                    if (första is Police && andra is Thief)
                    {
                        Police police = (Police)första;
                        Thief thief = (Thief)andra;
                        Gripa(police, thief, slump);
                    }

                    // Tjuv möter Citizen
                    if (första is Thief && andra is Citizen)
                    {
                        Thief thief = (Thief)första;
                        Citizen citizen = (Citizen)andra;
                        Råna(thief, citizen, slump);
                    }


                }
            }
        }

        // Om tjuv har stulit saker, griper polisen tjuven
        private void Gripa(Police police, Thief thief, Random slump)
        {
            // Räkna antal av stulna saker, eftersom det avgör fängelse tid
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

            // 10 sekunder per sak
            thief.Frislapps = DateTime.Now.AddSeconds(antal * 10);
            thief.BytPlats(Prison, slump);

            AntalGripanden++;
            Händelser.Add(police.Namn + " griper " + thief.Namn + ". Straff: " + (antal * 10) + " sekunder.");
        }

        // Tjuven tar en slumpad sak av en Citizen
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

            if (!rånadeCitizens.Contains(citizen))
            {
                rånadeCitizens.Add(citizen);
                AntalRånade++;
            }

            Händelser.Add(thief.Namn + " rånar " + citizen.Namn + " och tar " + sak.Namn + ".");

            // Om Citizen har slut på saker, åker in i fattighuset.
            if (citizen.Inventory.Count == 0)
            {
                citizen.BytPlats(PoorHouse, slump);
                Händelser.Add(citizen.Namn + " flyttar till fattighuset.");
            }
        }
    }
}
