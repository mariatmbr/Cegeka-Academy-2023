using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Configurations
{
	public class FundraiserConfiguration: IEntityTypeConfiguration<Fundraiser>
    {
		public FundraiserConfiguration()
		{
		}

        public void Configure(EntityTypeBuilder<Fundraiser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Donors);
            builder.Property(x => x.DonationTargetInRons).IsRequired();
            builder.Property(x => x.DonationTotal).IsRequired();

        }
    }
}

