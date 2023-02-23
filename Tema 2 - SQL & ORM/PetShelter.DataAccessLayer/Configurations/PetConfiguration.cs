using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Configurations
{
	public class PetConfiguration: IEntityTypeConfiguration<Pet>
    {
		public PetConfiguration()
		{
		}

        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
            builder.Property(x => x.IsHealthy).IsRequired().HasDefaultValue(true);
            builder.Property(x => x.WeightInKg).IsRequired();
            builder.Property(x => x.IsSheltered).IsRequired().HasDefaultValue(true);
            builder.HasOne(x => x.Rescuer).WithMany(x => x.RescuedPets).HasForeignKey(x => x.RescuerId).IsRequired(false);
            builder.HasOne(x => x.Adopter).WithMany(x => x.AdoptedPets).HasForeignKey(x => x.AdopterId).IsRequired(false);
        }
    }
}

