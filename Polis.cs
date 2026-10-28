using System;
using System;
using System.Collections.Generic;
using System.Text;
using TjuvOchPolis2;

namespace TjuvOchPolis
{
    // Klassen beskriver en polis.
    public class Police : Person
    {
        // Skapar en ny polis.
        public Police(string namn, Place plats, Random slump)
            : base(namn, plats, slump)
        {
            // Polisen visas med bokstaven P i staden.
            Symbol = 'P';
        }

        // Låter polisen gripa en tjuv.
        public string Grip(Thief thief, Prison prison, Random slump)
        {
            // Räknar hur många saker tjuven har.
            int antal = thief.Inventory.Count;

            // Kontrollera om tjuven har några saker.
            if (antal == 0)
            {
                return Namn + " griper " + thief.Namn
                    + ", men tjuven har inga saker.";
            }

            // Polisen beslagtar alla saker från tjuven.
            foreach (Item sak in thief.Inventory)
            {
                Inventory.Add(sak);
            }

            // Tömmer tjuvens lista på saker.
            thief.Inventory.Clear();

            // Straffet blir 10 sekunder per beslagtaget föremål.
            thief.Frislapps = DateTime.Now.AddSeconds(antal * 10);

            // Flyttar tjuven till fängelset.
            thief.BytPlats(prison, slump);

            // Berättar vad som hände och hur långt straffet är.
            return Namn + " griper " + thief.Namn
                + ". Straff: " + antal * 10 + " sekunder.";
        }
    }
}
