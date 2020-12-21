﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SaintNicholas.Data;

namespace SaintNicholas.Data.Migrations
{
    [DbContext(typeof(SaintNicholasDbContext))]
    [Migration("20201219155414_Age")]
    partial class Age
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("SaintNicholas.Data.BehavioralRecord", b =>
                {
                    b.Property<int>("ChildID")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.Property<bool>("Naughty")
                        .HasColumnType("bit");

                    b.HasKey("ChildID", "Year");

                    b.ToTable("BehavioralRecords");
                });

            modelBuilder.Entity("SaintNicholas.Data.Child", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("BirthYear")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.ToTable("Children");
                });

            modelBuilder.Entity("SaintNicholas.Data.ChristmasPresent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AgeGroup")
                        .HasColumnType("int");

                    b.Property<string>("Contents")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<bool>("ForNaughtyChild")
                        .HasColumnType("bit");

                    b.Property<int>("HandOutYear")
                        .HasColumnType("int");

                    b.Property<int?>("Receiver")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ChristmasPresents");
                });

            modelBuilder.Entity("SaintNicholas.Data.BehavioralRecord", b =>
                {
                    b.HasOne("SaintNicholas.Data.Child", "Child")
                        .WithMany()
                        .HasForeignKey("ChildID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Child");
                });
#pragma warning restore 612, 618
        }
    }
}
