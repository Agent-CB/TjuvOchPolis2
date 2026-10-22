using System;
using System.Collections.Generic;
using System.Text;


namespace TjuvOchPolis2
{
    public class Place
    {
        public string Namn { get; set; }
        public int Bredd { get; set; }
        public int Höjd { get; set; }
        public bool HarVägg { get; set; }

        public Place(string namn, int bredd, int höjd, bool harVägg)
        {
            Namn = namn;
            Bredd = bredd;
            Höjd = höjd;
            HarVägg = harVägg;
        }
    }
}
