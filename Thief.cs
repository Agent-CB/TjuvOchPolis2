using System;
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

        }
    }
}
ej klar ej klark