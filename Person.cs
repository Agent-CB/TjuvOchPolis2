
using System;
using System.Collections.Generic;

namespace TjuvOchPolis2
{
    // Klassen innehåller det som alla personer har gemensamt.
    public class Person
    {
        public string Namn { get; set; }
        public char Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int RiktningX { get; set; }
        public int RiktningY { get; set; }
        public Place Plats { get; set; }
        internal List<Item> Inventory { get; set; }

        // Skapar en person med namn och plats.
        public Person(string namn, Place plats, Random slump)
        {
            Namn = namn;
            Plats = plats;
            Inventory = new List<Item>();

            // Ger personen en startposition och riktning.
            BytPlats(plats, slump);
        }

        // Byter plats och väljer en ny position.
        public void BytPlats(Place plats, Random slump)
        {
            Plats = plats;

            // Slumpar fram personens position.
            X = slump.Next(0, plats.Bredd);
            Y = slump.Next(0, plats.Höjd);

            SlumpaRiktning(slump);
        }

        // Väljer en slumpmässig riktning för personen.
        private void SlumpaRiktning(Random slump)
        {
            int riktning = slump.Next(0, 6);

            switch (riktning)
            {
                case 0:
                    RiktningX = -1;
                    RiktningY = 0;
                    break;

                case 1:
                    RiktningX = 1;
                    RiktningY = 0;
                    break;

                case 2:
                    RiktningX = 0;
                    RiktningY = -1;
                    break;

                case 3:
                    RiktningX = 0;
                    RiktningY = 1;
                    break;

                case 4:
                    RiktningX = -1;
                    RiktningY = -1;
                    break;

                case 5:
                    RiktningX = 1;
                    RiktningY = 1;
                    break;
            }
        }

        // Flyttar personen ett steg i sin riktning.
        public void Flytta(Random slump)
        {
            // Ibland ändrar personen sin riktning.
            if (slump.Next(0, 5) == 0)
            {
                SlumpaRiktning(slump);
            }

            // Flyttar personen utifrån den nuvarande riktningen.
            X += RiktningX;
            Y += RiktningY;

            // Kontrollerar om platsen har väggar.
            if (Plats.HarVägg)
            {
                // Personen vänder om den träffar en sidovägg.
                if (X < 0 || X >= Plats.Bredd)
                {
                    RiktningX = -RiktningX;
                    X += RiktningX;
                }

                // Personen vänder om den träffar en övre eller nedre vägg.
                if (Y < 0 || Y >= Plats.Höjd)
                {
                    RiktningY = -RiktningY;
                    Y += RiktningY;
                }
            }
            else
            {
                // Om platsen saknar väggar kommer personen ut på andra sidan.
                if (X < 0)
                {
                    X = Plats.Bredd - 1;
                }
                else if (X >= Plats.Bredd)
                {
                    X = 0;
                }

                if (Y < 0)
                {
                    Y = Plats.Höjd - 1;
                }
                else if (Y >= Plats.Höjd)
                {
                    Y = 0;
                }
            }
        }
    }
}
