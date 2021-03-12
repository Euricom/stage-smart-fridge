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
            model.Entity<Amounts>().HasData(new Amounts { Id = 1, Name = "Cola-blik", Amount = 0 },
               new Amounts { Id = 2, Name = "Fanta", Amount = 0 },
               new Amounts { Id = 3, Name = "Sprite-Lemon-blik", Amount = 0 });

        }
    }
}
