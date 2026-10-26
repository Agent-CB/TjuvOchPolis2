using System;
using System.Linq.Expressions;
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
            // Tjuven visas med bokstaven T i staden.
            Symbol = 'T';
        }


        // Låter tjuven råna en medborgare.
        public string Råna(Citizen citizen, Random slump)

        //Låter tjuven råna en medborgare
        public string Rana(Citizen citizen, Random slumo)

        {
            // Kontrollera om medborgaren har några saker kvar.
            if (citizen.Inventory.Count == 0)
            {
                return "";
            }


            // Välj ett slumpmässigt föremål från medborgarens lista.
            int index = slump.Next(citizen.Inventory.Count);
            Item sak = citizen.Inventory[index];

            // Ta bort föremålet från medborgarens lista.
            citizen.Inventory.RemoveAt(index);

            // Lägg det stulna föremålet i tjuvens lista.
            Inventory.Add(sak);

            // Markera att medborgaren har blivit rånad.
            citizen.HarBlivitRånad = true;

            // Returnera ett meddelande om rånet.
            return Namn + " rånar " + citizen.Namn
                + " och tar " + sak.Namn + ".";

        }
    }
}
