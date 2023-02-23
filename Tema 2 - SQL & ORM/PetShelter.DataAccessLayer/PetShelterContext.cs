using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PetShelter.DataAccessLayer.Configurations;
using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer
{
	public class PetShelterContext : DbContext
	{
        public DbSet<Pet> Pets { get; set; }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Donation> Donations { get; set; }

        public DbSet<Fundraiser> Fundraisers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "server=localhost;Database=Cegeka;uid=test;password=parola;";

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 31));

            optionsBuilder.UseMySql(connectionString, serverVersion);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PetConfiguration());
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new DonationConfiguration());
            modelBuilder.ApplyConfiguration(new FundraiserConfiguration());
        }

    }
}

