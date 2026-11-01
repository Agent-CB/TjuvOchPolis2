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

.
        private List<Medborgare> rånadeMedborgare = new List<Medborgare>();

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
            Medborgare medborgare = new Medborgare("Medborgare " + (Personer.Count + 1), this, slump);
            Personer.Add(medborgare);
            Händelser.Add(medborgare.Namn + " flyttar in i staden.");
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

                    // Tjuv möter medborgare
                    if (första is Thief && andra is Medborgare)
                    {
                        Thief thief = (Thief)första;
                        Medborgare medborgare = (Medborgare)andra;
                        Råna(thief, medborgare, slump);
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

        // Tjuven tar en slumpad sak av medborgaren
        private void Råna(Thief thief, Medborgare medborgare, Random slump)
        {
            if (medborgare.Inventory.Count == 0)
            {
                return;
            }

            int index = slump.Next(medborgare.Inventory.Count);
            Item sak = medborgare.Inventory[index];
            medborgare.Inventory.RemoveAt(index);
            thief.Inventory.Add(sak);

            if (!rånadeMedborgare.Contains(medborgare))
            {
                rånadeMedborgare.Add(medborgare);
                AntalRånade++;
            }

            Händelser.Add(thief.Namn + " rånar " + medborgare.Namn + " och tar " + sak.Namn + ".");

            // Om medborgare har slut på saker, åker in i fattighuset.
            if (medborgare.Inventory.Count == 0)
            {
                medborgare.BytPlats(PoorHouse, slump);
                Händelser.Add(medborgare.Namn + " flyttar till fattighuset.");
            }
        }
    }
}