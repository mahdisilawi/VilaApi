﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vila.WebApi.Context;

#nullable disable

namespace Vila.WebApi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240414111223_AddCustomerTbl")]
    partial class AddCustomerTbl
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Vila.WebApi.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Pass")
                        .IsRequired()
                        .HasMaxLength(90)
                        .HasColumnType("nvarchar(90)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Vila.WebApi.Models.Detail", b =>
                {
                    b.Property<int>("DetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DetailId"));

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("VilaId")
                        .HasColumnType("int");

                    b.Property<string>("What")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("DetailId");

                    b.HasIndex("VilaId");

                    b.ToTable("Details");
                });

            modelBuilder.Entity("Vila.WebApi.Models.Vila", b =>
                {
                    b.Property<int>("VilaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VilaId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("BuildDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<long>("DayPrice")
                        .HasColumnType("bigint");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<long>("SellPrice")
                        .HasColumnType("bigint");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("VilaId");

                    b.ToTable("Vilas");
                });

            modelBuilder.Entity("Vila.WebApi.Models.Detail", b =>
                {
                    b.HasOne("Vila.WebApi.Models.Vila", "Vila")
                        .WithMany("Details")
                        .HasForeignKey("VilaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vila");
                });

            modelBuilder.Entity("Vila.WebApi.Models.Vila", b =>
                {
                    b.Navigation("Details");
                });
#pragma warning restore 612, 618
        }
    }
}
