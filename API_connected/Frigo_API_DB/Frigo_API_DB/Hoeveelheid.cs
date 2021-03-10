using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frigo_API_DB
{
    public class Hoeveelheid
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public int Aantal { get; set; }

        public Hoeveelheid()
        {

        }
        public Hoeveelheid(int id, string name, int amount)
        {
            Id = id;
            Naam = name;
            Aantal = amount;
        }
    }
}
