using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Configurations
{
	public class DonationConfiguration: IEntityTypeConfiguration<Donation>
	{
		public DonationConfiguration() { }

        public void Configure(EntityTypeBuilder<Donation> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Amount).IsRequired();
            builder.HasOne(x => x.Donor).WithMany(x => x.Donations).HasForeignKey(x => x.DonorId).IsRequired();
        }
    }
}

