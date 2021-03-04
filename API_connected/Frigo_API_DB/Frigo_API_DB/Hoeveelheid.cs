using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frigo_API_DB
{
    public class Hoeveelheid
    {
        public int id { get; set; }
        public string naam { get; set; }
        public int Aantal { get; set; }

        public Hoeveelheid()
        {

        }
        public Hoeveelheid(int i, string n, int a)
        {
            id = i;
            naam = n;
            Aantal = a;
        }
    }
}
