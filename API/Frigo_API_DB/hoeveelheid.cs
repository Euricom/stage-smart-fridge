using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frigo_API_DB
{
    public class hoeveelheid
    {
        public int id { get; set; }
        public string naam { get; set; }
        public int aantal { get; set; }

        public hoeveelheid(int i, string n, int a)
        {
            id = i;
            naam = n;
            aantal = a;
        }
    }
}
