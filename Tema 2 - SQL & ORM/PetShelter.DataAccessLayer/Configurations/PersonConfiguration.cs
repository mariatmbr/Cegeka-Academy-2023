using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Configurations
{
	public class PersonConfiguration: IEntityTypeConfiguration<Person>
	{
		public PersonConfiguration()
		{
		}

        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
            builder.Property(x => x.IdNumber).IsRequired().HasMaxLength(50);
        }
    }
}

