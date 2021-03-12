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

        public DbSet<Amounts> Hoeveelheden { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            var hoeveelheid = modelBuilder.Entity<Amounts>();
            hoeveelheid.ToTable("Hoeveelheden");
            hoeveelheid.HasKey("Id").IsClustered();
            hoeveelheid.Property("Id").ValueGeneratedOnAdd().IsRequired().HasColumnName("Id");
            hoeveelheid.Property(nameof(Amounts.Amount)).IsRequired().HasColumnName("Aantal");
            hoeveelheid.Property(nameof(Amounts.Name)).IsRequired().HasColumnName("Naam");

            modelBuilder.Seed();


        }

    }
}
