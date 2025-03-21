﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using mushrooms_ml.Data;

#nullable disable

namespace mushrooms_ml.Migrations
{
    [DbContext(typeof(MushroomDbContext))]
    [Migration("20250317172926_AddSaleFieldsToMushroom")]
    partial class AddSaleFieldsToMushroom
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("mushrooms_ml.Models.Mushroom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Bruises")
                        .HasColumnType("TEXT");

                    b.Property<string>("CapColor")
                        .HasColumnType("TEXT");

                    b.Property<string>("CapShape")
                        .HasColumnType("TEXT");

                    b.Property<string>("CapSurface")
                        .HasColumnType("TEXT");

                    b.Property<string>("GillAttachment")
                        .HasColumnType("TEXT");

                    b.Property<string>("GillColor")
                        .HasColumnType("TEXT");

                    b.Property<string>("GillSize")
                        .HasColumnType("TEXT");

                    b.Property<string>("GillSpacing")
                        .HasColumnType("TEXT");

                    b.Property<string>("Habitat")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsForSale")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Odor")
                        .HasColumnType("TEXT");

                    b.Property<string>("Poisonous")
                        .HasColumnType("TEXT");

                    b.Property<string>("Population")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("Price")
                        .HasColumnType("TEXT");

                    b.Property<string>("RingNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("RingType")
                        .HasColumnType("TEXT");

                    b.Property<string>("SporePrintColor")
                        .HasColumnType("TEXT");

                    b.Property<string>("StalkColorAboveRing")
                        .HasColumnType("TEXT");

                    b.Property<string>("StalkColorBelowRing")
                        .HasColumnType("TEXT");

                    b.Property<string>("StalkRoot")
                        .HasColumnType("TEXT");

                    b.Property<string>("StalkShape")
                        .HasColumnType("TEXT");

                    b.Property<string>("StalkSurfaceAboveRing")
                        .HasColumnType("TEXT");

                    b.Property<string>("StalkSurfaceBelowRing")
                        .HasColumnType("TEXT");

                    b.Property<string>("VeilColor")
                        .HasColumnType("TEXT");

                    b.Property<string>("VeilType")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Mushrooms");
                });
#pragma warning restore 612, 618
        }
    }
}
