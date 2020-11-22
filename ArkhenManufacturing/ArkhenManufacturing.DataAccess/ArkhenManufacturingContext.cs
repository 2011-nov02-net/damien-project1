using System;

using Microsoft.EntityFrameworkCore;

namespace ArkhenManufacturing.DataAccess
{
    public class ArkhenManufacturingContext : DbContext
    {
        public ArkhenManufacturingContext(DbContextOptions<ArkhenManufacturingContext> options) :
            base(options) {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<InventoryEntry> InventoryEntries { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<Address>(entity => {
                entity.ToTable("Address");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever();

                entity.Property(e => e.Line1)
                    .IsRequired();

                entity.Property(e => e.City)
                    .IsRequired();

                entity.Property(e => e.Country)
                    .IsRequired();

                entity.Property(e => e.ZipCode)
                    .IsRequired();
            });

            modelBuilder.Entity<Admin>(entity => {
                entity.ToTable("Admin");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever();

                entity.Property(e => e.UserName)
                    .IsRequired();

                entity.Property(e => e.LastName)
                    .IsRequired();

                entity.Property(e => e.FirstName)
                    .IsRequired();

                entity.Property(e => e.Password)
                    .IsRequired();

                entity.Property(e => e.Email)
                    .IsRequired();

                entity.HasIndex(e => e.UserName)
                    .IsUnique();

                entity.HasIndex(e => e.Email)
                    .IsUnique();

                entity.HasMany(e => e.Orders)
                    .WithOne(e => e.Admin)
                    .HasForeignKey(e => e.AdminId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AdminOrder_AdminId");

                // TODO: Set up N:N for Locations
            });

            modelBuilder.Entity<Customer>(entity => {
                entity.ToTable("Customer");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever();

                entity.Property(e => e.UserName)
                    .IsRequired();

                entity.Property(e => e.LastName)
                    .IsRequired();

                entity.Property(e => e.FirstName)
                    .IsRequired();

                entity.Property(e => e.Password)
                    .IsRequired();

                entity.Property(e => e.Email)
                    .IsRequired();

                entity.Property(e => e.BirthDate)
                    .IsRequired();

                entity.Property(e => e.SignUpDate)
                    .IsRequired();

                entity.HasIndex(e => e.UserName)
                    .IsUnique();

                entity.HasIndex(e => e.Email)
                    .IsUnique();

                // TODO: Set up FK to Address
                // TODO: Set up nullable FK to DefaultLocation
                // TODO: Set up 1:N with orders
            });

            modelBuilder.Entity<InventoryEntry>(entity => {
                entity.ToTable("InventoryEntry");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever();

                entity.Property(e => e.Price)
                    .IsRequired();

                // TODO: Set up FK to Product
                // TODO: Set up FK to Location
            });

            modelBuilder.Entity<Location>(entity => {
                entity.ToTable("Location");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .IsRequired();

                // TODO: Set up N:N to Admin
                // TODO: Set up 1:N to Orders
                // TODO: Set up 1:N to InventoryEntries
            });

            modelBuilder.Entity<Order>(entity => {
                entity.ToTable("Order");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever();

                entity.Property(e => e.PlacementDate)
                    .IsRequired();

                // TODO: Set up FK to Customer
                // TODO: Set up FK to Admin
                // TODO: Set up FK to Location
                // TODO: Set up 1:N to OrderLines
            });

            modelBuilder.Entity<OrderLine>(entity => {
                entity.ToTable("OrderLine");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever();

                entity.Property(e => e.Count)
                    .IsRequired();

                entity.Property(e => e.PricePerUnit)
                    .IsRequired();

                // TODO: Set up FK to Order
                // TODO: Set up FK to Product
            });

            modelBuilder.Entity<Product>(entity => {
                entity.ToTable("Product");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired();
            });
        }
    }
}
