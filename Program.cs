using System.Drawing;

namespace TjuvOchPolis2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random slump = new Random();
            City stad = new City();
            Screen skärm = new Screen(stad);

            for (int i = 0; i < 20; i++)
            {
                stad.LäggTillCitizen(slump);
            }

            for (int i = 0; i < 10; i++)
            {
                stad.LäggTillThief(slump);
            }

            for (int i = 0; i < 5; i++)
            {
                stad.LäggTillPolice(slump);
            }

            bool kör = true;
            bool inventory = false;
            DateTime pausTill = DateTime.Now;

            Console.Clear();
            Console.CursorVisible = false;


            while (kör)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKey tangent = Console.ReadKey(true).Key;

                    if (tangent == ConsoleKey.Escape)
                    {
                        kör = false;
                    }

                    if (tangent == ConsoleKey.C)
                    {
                        stad.LäggTillCitizen(slump);
                    }

                    if (tangent == ConsoleKey.T)
                    {
                        stad.LäggTillThief(slump);
                    }

                    if (tangent == ConsoleKey.P)
                    {
                        stad.LäggTillPolice(slump);
                    }

                    if (tangent == ConsoleKey.I)
                    {
                        inventory = !inventory;
                    }

                    if (inventory && tangent == ConsoleKey.RightArrow)
                    {
                        skärm.BytSida(1);
                    }

                    if (inventory && tangent == ConsoleKey.LeftArrow)
                    {
                        skärm.Bytsida(-1);
                    }
                }
                    
            }
        }
    }
}
