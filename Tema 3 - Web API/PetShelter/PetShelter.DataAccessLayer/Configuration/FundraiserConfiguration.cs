using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetShelter.DataAccessLayer.Models;

public class FundraiserConfiguration : IEntityTypeConfiguration<Fundraiser>
{
        public void Configure(EntityTypeBuilder<Fundraiser> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(256);
            builder.Property(p => p.GoalValue).IsRequired();
            builder.Property(p => p.OwnerId).IsRequired();
            builder.Property(p=>p.DueDate).IsRequired();
            builder.HasMany(p => p.Donations).WithOne(p => p.Fundraiser)
                .HasForeignKey(p => p.FundraiserId);
            builder.HasOne(p => p.Owner).WithMany(p => p.Fundraisers).HasForeignKey(p => p.OwnerId);
        }
}
