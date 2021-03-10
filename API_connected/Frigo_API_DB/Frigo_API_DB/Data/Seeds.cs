using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frigo_API_DB.Data
{
    public static class Seeds
    {
        public static void Seed(this ModelBuilder model)
        {
            model.Entity<Hoeveelheid>().HasData(new Hoeveelheid { Id = 1, Naam = "Cola-blik", Aantal = 0 },
               new Hoeveelheid { Id = 2, Naam = "Fanta", Aantal = 0 },
               new Hoeveelheid { Id = 3, Naam = "Sprite-Lemon-blik", Aantal = 0 });

        }
    }
}
