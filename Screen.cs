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
                SkrivRad("X" + new string(' ', plats.Bredd) + "X");
            }

            SkrivRad(new string('=', plats.Bredd + 2));

            foreach (Person person in stad.personer)
            {
                if (person.Plats != plats)
                {
                    continue;
                }

                if (person.X + 1 >= Console.WindowWidth || förstaRad + person.Y >= Console.WindowHeight)
                {
                    continue;
                }

                Console.SetCursorPosition(person.X + 1, förstaRad + person.Y);
                
                if (person.Symbol == 'C')
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                if (person.Symbol == 'T')
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                if (person.Symbol == 'P')
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }

                Console.Write(person.Symbol);
            }
        }




        public bool FinnsPlats(bool inventory)
        {
            int höjd = 66;

            if (inventory)
            {
                höjd = 20;
            }

            if (Console.WindowWidth >= 100 && Console.WindowHeight >= höjd)
            {
                return true;
            }

            Console.Clear();
            rad = 0;

            SkrivRad("Fönstret är för litet. Vänligen förstora fönstret.");
            
            return false;
        }




        public void BytSida(int steg)
        {
            int perSida = Console.WindowHeight - 16;

            if (perSida <= 0)
            {
                return;
            }

            sida = sida + steg;

            if (sida < 0)
            {
                sida = 0;
            }

            int sistaSida = (stad.Personer.Count - 1) / perSida;

            if (sida > sistaSida)
            {
                sida = sistaSida;
            }
        }


    }
}
