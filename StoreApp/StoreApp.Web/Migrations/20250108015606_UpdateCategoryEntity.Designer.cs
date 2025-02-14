﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StoreApp.Data.Concrete;

#nullable disable

namespace StoreApp.Web.Migrations
{
    [DbContext(typeof(StoreDbContext))]
    [Migration("20250108015606_UpdateCategoryEntity")]
    partial class UpdateCategoryEntity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("StoreApp.Data.Concrete.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CategoryUrl")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("CategoryId");

                    b.HasIndex("CategoryUrl")
                        .IsUnique();

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            CategoryName = "Telefon",
                            CategoryUrl = "telefon"
                        },
                        new
                        {
                            CategoryId = 2,
                            CategoryName = "Bilgisayar",
                            CategoryUrl = "bilgisayar"
                        },
                        new
                        {
                            CategoryId = 3,
                            CategoryName = "Elektronik",
                            CategoryUrl = "elektronik"
                        },
                        new
                        {
                            CategoryId = 4,
                            CategoryName = "Beyaz Eşya",
                            CategoryUrl = "beyaz-esya"
                        },
                        new
                        {
                            CategoryId = 5,
                            CategoryName = "Mobilya",
                            CategoryUrl = "mobilya"
                        },
                        new
                        {
                            CategoryId = 6,
                            CategoryName = "Kırtasiye",
                            CategoryUrl = "kirtasiye"
                        });
                });

            modelBuilder.Entity("StoreApp.Data.Concrete.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("ProductId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            Description = "Samsung Türkiye Garantili",
                            Name = "Samsung S24",
                            Price = 50000m
                        },
                        new
                        {
                            ProductId = 2,
                            Description = "Samsung Türkiye Garantili",
                            Name = "Samsung S25",
                            Price = 60000m
                        },
                        new
                        {
                            ProductId = 3,
                            Description = "Samsung Türkiye Garantili",
                            Name = "Samsung S26",
                            Price = 70000m
                        },
                        new
                        {
                            ProductId = 4,
                            Description = "Samsung Türkiye Garantili",
                            Name = "Samsung S27",
                            Price = 80000m
                        },
                        new
                        {
                            ProductId = 5,
                            Description = "Samsung Türkiye Garantili",
                            Name = "Samsung S28",
                            Price = 90000m
                        },
                        new
                        {
                            ProductId = 6,
                            Description = "Samsung Türkiye Garantili",
                            Name = "Samsung S29",
                            Price = 100000m
                        },
                        new
                        {
                            ProductId = 7,
                            Description = "Güzel Makine",
                            Name = "Arçelik Çamaşır Makinesi",
                            Price = 110000m
                        });
                });

            modelBuilder.Entity("StoreApp.Data.Concrete.ProductCategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("CategoryId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductCategory");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            ProductId = 1
                        },
                        new
                        {
                            CategoryId = 1,
                            ProductId = 2
                        },
                        new
                        {
                            CategoryId = 1,
                            ProductId = 3
                        },
                        new
                        {
                            CategoryId = 1,
                            ProductId = 4
                        },
                        new
                        {
                            CategoryId = 1,
                            ProductId = 5
                        },
                        new
                        {
                            CategoryId = 1,
                            ProductId = 6
                        },
                        new
                        {
                            CategoryId = 4,
                            ProductId = 7
                        });
                });

            modelBuilder.Entity("StoreApp.Data.Concrete.ProductCategory", b =>
                {
                    b.HasOne("StoreApp.Data.Concrete.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoreApp.Data.Concrete.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
