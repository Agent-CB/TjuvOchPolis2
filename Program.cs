using System.Drawing;

namespace TjuvOchPolis2
{
    internal class Program
    {
        static void Main(string[] args)
        {
           // Skapar slumpgeneratorn, stad och skärmen.
            Random slump = new Random();
            City stad = new City();
            Screen skärm = new Screen(stad);

            // Lägger till 20 medborgare i staden.
            for (int i = 0; i < 20; i++)
            {
                stad.LäggTillCitizen(slump);
            }

            // Lägger till 10 tjuvar i staden.
            for (int i = 0; i < 10; i++)
            {
                stad.LäggTillThief(slump);
            }

            // Lägger till 5 poliser i staden.
            for (int i = 0; i < 5; i++)
            {
                stad.LäggTillPolice(slump);
            }


            // Programmet ska köras och inventory ska vara stängt från början.
            bool kör = true;
            bool inventory = false;

            
            // Sparar tiden som används för att styra pausen.
            DateTime pausTill = DateTime.Now;

            // Tömmer konsolen och gömmer markören.
            Console.Clear();
            Console.CursorVisible = false;




            // Hanterar tangenterna för att lägga till personer, visa inventory, byta sida och avsluta programmet.
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

                    if (!kör)
                    {
                        break;
                    }

                    if (!skärm.FinnsPlats(inventory))
                    {
                        Thread.Sleep(100);
                        continue;   
                    }

                    stad.Prison.Frigefångar(stad, slump);

                    // Flyttar personerna och kontrollerar möten när pausen är slut.
                    if (DateTime.Now >= pausTill)
                    {
                        int antalFöre = stad.Händelser.Count;
                        stad.FlyttaAlla(slump);
                        stad.KontrolleraMöten(slump);

                        // Pausar rörelserna i två sekunder om något nytt har hänt.
                        if (stad.Händelser.Count > antalFöre)
                        {
                            pausTill = DateTime.Now.AddSeconds(2);
                        }
                    }
                }
                // Ritar om skärmen och väntar en liten stund.
                 skärm.Rita(inventory);
                Thread.Sleep(100);
            }
            // Återställer konsolen och sparar händelserna i en textfil när programmet avslutas.
            Console.ResetColor();
            Console.CursorVisible = true;
            Console.Clear();
            File.WriteAllLines("Händelser.txt", stad.Händelser.ToArray());
            Console.WriteLine("Händelserna sparades i händelser.txt");
        }
    }
}
