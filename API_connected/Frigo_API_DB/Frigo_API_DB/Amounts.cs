using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frigo_API_DB
{
    public class Amounts
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }

        public Amounts()
        {

        }
        public Amounts(int id, string name, int amount)
        {
            Id = id;
            Name = name;
            Amount = amount;
        }
    }
}
