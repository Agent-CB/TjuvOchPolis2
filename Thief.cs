using System;
using System.Numerics;

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
//ej klar ej klark