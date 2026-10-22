using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using TjuvOchPolis2;

namespace TjuvOchPolis
{
    // Klassen beskriver en medborgare i staden.
    public class Medborgare : Person
    {
        // Håller reda på om medborgaren har blivit rånad.
        public bool HarBlivitRanad { get; set; }

        // Skapar en ny medborgare.
        public Medborgare(string namn, Place plats, Random slump)
            : base(namn, plats, slump)
        {
            // Medborgaren visas med bokstaven M i staden.
            Symbol = 'M';

            // Alla medborgare börjar med fyra saker.
            Inventory.Add(new Item("Nycklar"));
            Inventory.Add(new Item("Mobil"));
            Inventory.Add(new Item("Pengar"));
            Inventory.Add(new Item("Klocka"));
        }
    }
}