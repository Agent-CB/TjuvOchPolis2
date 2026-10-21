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
    }
}
