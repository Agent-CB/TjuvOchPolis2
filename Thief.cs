using System;
using System.Linq.Expressions;
using System.Numerics;
using TjuvOchPolis2;

namespace TjuvOchPolis
{
    // Klassen beskriver en tjuv.
    public class Thief : Person
    {
        // Håller reda på när tjuven kan friges.
        public DateTime Frislapps { get; set; }

        // Skapar en ny tjuv.
        public Thief(string namn, Place plats, Random slump)
            : base(namn, plats, slump)
        {
            // Tjuven visas med bokstaven T i staden.
            Symbol = 'T';
        }

        //Låter tjuven råna en medborgare
        public string Rana(Citizen citizen, Random slumo)
        {
            // Kontrollera om medborgaren har några saker kvar.
            if (citizen.Inventory.Count == 0)
            {
                return "";
            }
        }
    }
}
