﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PetShelter.DataAccessLayer;

#nullable disable

namespace PetShelter.DataAccessLayer.Migrations
{
    [DbContext(typeof(PetShelterContext))]
    [Migration("20230217092656_AddDonationsTable")]
    partial class AddDonationsTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PetShelter.DataAccessLayer.Models.Donation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("DonorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DonorId");

                    b.ToTable("Donations");
                });

            modelBuilder.Entity("PetShelter.DataAccessLayer.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("IdNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("PetShelter.DataAccessLayer.Models.Pet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AdopterId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsHealthy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(true);

                    b.Property<bool>("IsSheltered")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<int?>("RescuerId")
                        .HasColumnType("int");

                    b.Property<decimal>("WeightInKg")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("AdopterId");

                    b.HasIndex("RescuerId");

                    b.ToTable("Pets");
                });

            modelBuilder.Entity("PetShelter.DataAccessLayer.Models.Donation", b =>
                {
                    b.HasOne("PetShelter.DataAccessLayer.Models.Person", "Donor")
                        .WithMany("Donations")
                        .HasForeignKey("DonorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Donor");
                });

            modelBuilder.Entity("PetShelter.DataAccessLayer.Models.Pet", b =>
                {
                    b.HasOne("PetShelter.DataAccessLayer.Models.Person", "Adopter")
                        .WithMany("AdoptedPets")
                        .HasForeignKey("AdopterId");

                    b.HasOne("PetShelter.DataAccessLayer.Models.Person", "Rescuer")
                        .WithMany("RescuedPets")
                        .HasForeignKey("RescuerId");

                    b.Navigation("Adopter");

                    b.Navigation("Rescuer");
                });

            modelBuilder.Entity("PetShelter.DataAccessLayer.Models.Person", b =>
                {
                    b.Navigation("AdoptedPets");

                    b.Navigation("Donations");

                    b.Navigation("RescuedPets");
                });
#pragma warning restore 612, 618
        }
    }
}