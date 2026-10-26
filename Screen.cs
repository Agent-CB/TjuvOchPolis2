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

        public void Rita(bool inventory)
        {
            rad = 0;

            if (inventory)
            {
                int perSida = Console.WindowHeight - 16;
                int antalSidor = (stad.Personer.Count + perSida - 1) / perSida;

                if (antalSidor == 0)
                {
                    antalSidor = 1;
                }

                if (sida >= antalSidor)
                {
                    sida = antalSidor - 1;
                }

                SkrivRubrik("Inventory - sida " + (sida + 1) + " av " + antalSidor);

                int start = sida  * perSida;
                int slut = start + perSida;

                if (slut > stad.Personer.Count)
                {
                    slut = stad.Personer.Count;
                }

                for (int i = start; i < slut; i++)
                {
                    Person person = stad.Personer[i];
                    string text = person.Namn + " | " + person.Plats.Namn + " | ";

                    foreach (Item sak in person.Inventory)
                    {
                        text = text + sak.Namn + " ";
                    }

                    SkrivRad(text);
                }
            }
            else
            {
                RitaPlats(stad);
                RitaPlats(stad.Prison);
                RitaPlats(stad.PoorHouse);
            }

            SkrivRubrik("STATUS");

            SkrivRad("Thief in City: " + stad.RäknaTyp('T') + " | In Prison " + stad.RäknaPåPlats(stad.Prison));

            SkrivRad("Citizen in City: " + stad.RäknaTyp('C') + " | In PoorHouse: " + stad.RäknaPåPlats(stad.PoorHouse));

            SkrivRad("Police: " + stad.RäknaTyp('P') + " | Rånade: " + stad.AntalRånade + " | Fripanden: " + stad.AntalGripanden);

            SkrivRubrik("NEWS FEED");

            for (int i = 0; i < 8; i++)
            {
                int index = stad.Händelser.Count - 1 - i;
                string text = "";

                if (index >= 0)
                {
                    text = (index + 1) + ": " + stad.Händelser[index];
                }

                SkrivRad(text);
            }

            if (inventory)
            {
                SkrivRad("Vänster/Höger: byt sida | I: Karta | Esc: Avsluta");
            }
            else
            {
                SkrivRad("C: Citizen | T: Thief | P: Police | I: Visa inventory | Esc: Avsluta");
            }

            while (rad < Console.WindowHeight)
            {
                SkrivRad("");
            }
        }    
    }
}
