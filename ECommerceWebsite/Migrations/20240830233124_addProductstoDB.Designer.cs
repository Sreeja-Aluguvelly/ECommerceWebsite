﻿// <auto-generated />
using ECommerce.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ECommerceWebsite.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20240830233124_addProductstoDB")]
    partial class addProductstoDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ECommerce.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayOrder = 1,
                            Name = "Fiction"
                        },
                        new
                        {
                            Id = 2,
                            DisplayOrder = 2,
                            Name = "Travel"
                        },
                        new
                        {
                            Id = 3,
                            DisplayOrder = 3,
                            Name = "Music"
                        });
                });

            modelBuilder.Entity("ECommerceWebsite.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ListPrice")
                        .HasColumnType("float");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<double>("Price100")
                        .HasColumnType("float");

                    b.Property<double>("Price50")
                        .HasColumnType("float");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Product");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Brand = "CozyWool",
                            Description = "A soft and warm 100% wool yarn bundle ideal for knitting scarves, sweaters, and blankets. This bundle includes 5 skeins in various colors, providing ample material for multiple projects.",
                            ListPrice = 49.990000000000002,
                            Price = 44.990000000000002,
                            Price100 = 39.990000000000002,
                            Price50 = 42.990000000000002,
                            ProductName = "CozyWool Knitting Yarn Bundle"
                        },
                        new
                        {
                            Id = 2,
                            Brand = "Artist’s Choice",
                            Description = "Premium watercolor paint set with 24 vibrant colors, perfect for both beginners and professionals. The high-pigment paints blend seamlessly, providing rich and smooth coverage for all your watercolor projects.",
                            ListPrice = 39.990000000000002,
                            Price = 34.990000000000002,
                            Price100 = 30.989999999999998,
                            Price50 = 32.990000000000002,
                            ProductName = "Watercolor Paint Set"
                        },
                        new
                        {
                            Id = 3,
                            Brand = "CandleCraft",
                            Description = "This DIY kit includes everything you need to create your own scented candles at home. The kit comes with soy wax, wicks, essential oils, and reusable containers, making it perfect for crafting unique, personalized candles.",
                            ListPrice = 59.990000000000002,
                            Price = 54.990000000000002,
                            Price100 = 49.990000000000002,
                            Price50 = 52.990000000000002,
                            ProductName = "CandleCraft DIY Candle Making Kit"
                        },
                        new
                        {
                            Id = 4,
                            Brand = "VibrantColors",
                            Description = "A versatile acrylic paint set with 12 tubes of richly pigmented colors, perfect for all types of art projects. These fast-drying paints are suitable for use on canvas, wood, fabric, and more.",
                            ListPrice = 29.989999999999998,
                            Price = 24.989999999999998,
                            Price100 = 20.989999999999998,
                            Price50 = 22.989999999999998,
                            ProductName = "VibrantColors Acrylic Paint Set"
                        },
                        new
                        {
                            Id = 5,
                            Brand = "KnitPro",
                            Description = " A set of 10 high-quality bamboo knitting needles in various sizes. Lightweight and smooth, these needles provide a comfortable knitting experience and are ideal for beginners and seasoned knitters alike.",
                            ListPrice = 39.990000000000002,
                            Price = 34.990000000000002,
                            Price100 = 29.989999999999998,
                            Price50 = 32.990000000000002,
                            ProductName = "KnitPro Bamboo Needle Set"
                        },
                        new
                        {
                            Id = 6,
                            Brand = "PureCraft",
                            Description = "Create your own luxurious soaps with this DIY kit. It includes natural soap bases, molds, essential oils, and colorants, allowing you to craft custom soaps at home with ease.",
                            ListPrice = 49.990000000000002,
                            Price = 44.990000000000002,
                            Price100 = 39.990000000000002,
                            Price50 = 42.990000000000002,
                            ProductName = "PureCraft DIY Soap Making Kit"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
