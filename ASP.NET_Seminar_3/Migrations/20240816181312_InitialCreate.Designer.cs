﻿// <auto-generated />
using System;
using ASP.NET_Seminar_3;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ASP.NET_Seminar_3.Migrations
{
    [DbContext(typeof(Seminar3Context))]
    [Migration("20240816181312_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ASP.NET_Seminar3.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id")
                        .HasName("PK_CategoryID");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Categories", (string)null);
                });

            modelBuilder.Entity("ASP.NET_Seminar3.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CategoryID")
                        .HasColumnType("int");

                    b.Property<int?>("Cost")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id")
                        .HasName("PK_ProductID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("ASP.NET_Seminar3.Models.ProductStorage", b =>
                {
                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("StorageID")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "StorageID")
                        .HasName("PK_ProductStorage");

                    b.HasIndex("StorageID");

                    b.ToTable("ProductStorages");
                });

            modelBuilder.Entity("ASP.NET_Seminar3.Models.Storage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id")
                        .HasName("PK_StorageID");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Storages", (string)null);
                });

            modelBuilder.Entity("ASP.NET_Seminar3.Models.Product", b =>
                {
                    b.HasOne("ASP.NET_Seminar3.Models.Category", "ProductGroup")
                        .WithMany("Products")
                        .HasForeignKey("CategoryID");

                    b.Navigation("ProductGroup");
                });

            modelBuilder.Entity("ASP.NET_Seminar3.Models.ProductStorage", b =>
                {
                    b.HasOne("ASP.NET_Seminar3.Models.Product", "Product")
                        .WithMany("ProductStorages")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ASP.NET_Seminar3.Models.Storage", "Storage")
                        .WithMany("ProductStorages")
                        .HasForeignKey("StorageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Storage");
                });

            modelBuilder.Entity("ASP.NET_Seminar3.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("ASP.NET_Seminar3.Models.Product", b =>
                {
                    b.Navigation("ProductStorages");
                });

            modelBuilder.Entity("ASP.NET_Seminar3.Models.Storage", b =>
                {
                    b.Navigation("ProductStorages");
                });
#pragma warning restore 612, 618
        }
    }
}