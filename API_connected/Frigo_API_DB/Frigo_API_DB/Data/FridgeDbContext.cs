using Frigo_API_DB.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frigo_API_DB.Data
{       //this is inherriting from dbContext class but does more now.
    public class FridgeDbContext: IdentityDbContext<Person>
    {
        public DbSet<Amounts> Hoeveelheden { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public FridgeDbContext(DbContextOptions<FridgeDbContext> options): base(options)
        {
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            var hoeveelheid = modelBuilder.Entity<Amounts>();
            hoeveelheid.ToTable("Hoeveelheden");
            hoeveelheid.HasKey("Id").IsClustered();
            hoeveelheid.Property("Id").ValueGeneratedOnAdd().IsRequired().HasColumnName("Id");
            hoeveelheid.Property(nameof(Amounts.Amount)).IsRequired().HasColumnName("Aantal");
            hoeveelheid.Property(nameof(Amounts.Name)).IsRequired().HasColumnName("Naam");

            modelBuilder.ApplyConfiguration(new SettingsConfiguration());
            //var person = modelBuilder.Entity<Person>();
            //person.ToTable("Persons");
            //person.HasKey("Id").IsClustered();
            //person.Property(nameof(Person.Id)).ValueGeneratedOnAdd().IsRequired().HasColumnName("Id");
            //person.Property(nameof(Person.Email)).IsRequired().HasColumnName("Name");
            //person.Property(nameof(Person.Password)).IsRequired().HasColumnName("Password");



            modelBuilder.Seed();


        }

    }
}
