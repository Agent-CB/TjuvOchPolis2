using System;
using System.Collections.Generic;
using System.Text;

namespace TjuvOchPolis2
{
    internal class Screen
    {
        private City stad;
        private int rad;
        private int sida = 0;

        public Screen(City stad)
        {
            this.stad = stad;
        }

        private void SkrivRad(string text)
        {
            int bredd = Console.WindowWidth - 1;

            if (bredd > 0 && rad < Console.WindowHeight)
            {
                if (text.Length > bredd)
                {
                    text = text.Substring(0, bredd);
                }

                Console.SetCursorPosition(0, rad);
                Console.ResetColor();
                Console.Write(text.PadRight(bredd));
            }
            rad++;
        }

        private void SkrivRubrik(string text)
        {
            string rubrik = "= " + text + " ";

            SkrivRad(rubrik + new string('=', 102 - rubrik.Length));
        }

        private void RitaPlats(Place plats)
        {
            SkrivRubrik(plats.Namn);
            int förstaRad = rad;

            for (int y = 0; y < plats.Höjd; y++)
            {
                SkrivRad("X" + new string(' ', plats.bredd) + "X");
            }

            SkrivRad(new string('=', plats.Bredd + 2));

            foreach (Person person in stad.personer)
            {

            }
        }
    }
}
