using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frigo_API_DB.Data
{
    public class FridgeDbContext: DbContext
    {
        public FridgeDbContext(DbContextOptions<FridgeDbContext> options): base(options)
        {
        }

        public DbSet<Hoeveelheid> Hoeveelheden { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var hoeveelheid = modelBuilder.Entity<Hoeveelheid>();
            hoeveelheid.ToTable("Hoeveelheden");
            hoeveelheid.HasKey("id").IsClustered();
            hoeveelheid.Property("id").ValueGeneratedOnAdd().IsRequired().HasColumnName("Id");
            hoeveelheid.Property(nameof(Hoeveelheid.Aantal)).IsRequired().HasColumnName("Aantal");


        }

    }
}
