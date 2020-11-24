﻿// <auto-generated />
using System;
using ArkhenManufacturing.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ArkhenManufacturing.DataAccess.Migrations
{
    [DbContext(typeof(ArkhenContext))]
    [Migration("20201124011743_LocationHasNameNow")]
    partial class LocationHasNameNow
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("ArkhenManufacturing.DataAccess.DbAddress", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Line1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Line2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("ArkhenManufacturing.DataAccess.DbAdmin", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("ArkhenManufacturing.DataAccess.DbCustomer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DefaultLocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SignUpDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("DefaultLocationId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("ArkhenManufacturing.DataAccess.DbInventoryEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(3,3)");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("ProductId");

                    b.ToTable("InventoryEntry");
                });

            modelBuilder.Entity("ArkhenManufacturing.DataAccess.DbLocation", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("ArkhenManufacturing.DataAccess.DbLocationAdmin", b =>
                {
                    b.Property<Guid>("LocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AdminId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LocationId", "AdminId");

                    b.HasIndex("AdminId");

                    b.ToTable("LocationAdmin");
                });

            modelBuilder.Entity("ArkhenManufacturing.DataAccess.DbOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AdminId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DbAdminId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DbLocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("PlacementDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("DbAdminId");

                    b.HasIndex("DbLocationId");

                    b.HasIndex("LocationId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("ArkhenManufacturing.DataAccess.DbOrderLine", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(3,3)");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("PricePerUnit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderLine");
                });

            modelBuilder.Entity("ArkhenManufacturing.DataAccess.DbProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("ArkhenManufacturing.DataAccess.DbCustomer", b =>
                {
                    b.HasOne("ArkhenManufacturing.DataAccess.DbAddress", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArkhenManufacturing.DataAccess.DbLocation", "DefaultLocation")
                        .WithMany()
                        .HasForeignKey("DefaultLocationId")
                        .OnDelete(DeleteBehavior.ClientNoAction)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("DefaultLocation");
                });

            modelBuilder.Entity("ArkhenManufacturing.DataAccess.DbInventoryEntry", b =>
                {
                    b.HasOne("ArkhenManufacturing.DataAccess.DbLocation", "Location")
                        .WithMany("InventoryEntries")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArkhenManufacturing.DataAccess.DbProduct", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ArkhenManufacturing.DataAccess.DbLocation", b =>
                {
                    b.HasOne("ArkhenManufacturing.DataAccess.DbAddress", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("ArkhenManufacturing.DataAccess.DbLocationAdmin", b =>
                {
                    b.HasOne("ArkhenManufacturing.DataAccess.DbAdmin", "Admin")
                        .WithMany("LocationAdmins")
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArkhenManufacturing.DataAccess.DbLocation", "Location")
                        .WithMany("LocationAdmins")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admin");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("ArkhenManufacturing.DataAccess.DbOrder", b =>
                {
                    b.HasOne("ArkhenManufacturing.DataAccess.DbAdmin", "Admin")
                        .WithMany()
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArkhenManufacturing.DataAccess.DbCustomer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArkhenManufacturing.DataAccess.DbAdmin", null)
                        .WithMany("Orders")
                        .HasForeignKey("DbAdminId");

                    b.HasOne("ArkhenManufacturing.DataAccess.DbLocation", null)
                        .WithMany("Orders")
                        .HasForeignKey("DbLocationId");

                    b.HasOne("ArkhenManufacturing.DataAccess.DbLocation", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.ClientNoAction)
                        .IsRequired();

                    b.Navigation("Admin");

                    b.Navigation("Customer");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("ArkhenManufacturing.DataAccess.DbOrderLine", b =>
                {
                    b.HasOne("ArkhenManufacturing.DataAccess.DbOrder", "Order")
                        .WithMany("OrderLines")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArkhenManufacturing.DataAccess.DbProduct", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ArkhenManufacturing.DataAccess.DbAdmin", b =>
                {
                    b.Navigation("LocationAdmins");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("ArkhenManufacturing.DataAccess.DbCustomer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("ArkhenManufacturing.DataAccess.DbLocation", b =>
                {
                    b.Navigation("InventoryEntries");

                    b.Navigation("LocationAdmins");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("ArkhenManufacturing.DataAccess.DbOrder", b =>
                {
                    b.Navigation("OrderLines");
                });
#pragma warning restore 612, 618
        }
    }
}
