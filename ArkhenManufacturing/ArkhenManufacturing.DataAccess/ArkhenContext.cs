using System;

using Microsoft.EntityFrameworkCore;

namespace ArkhenManufacturing.DataAccess
{
    public class ArkhenContext : DbContext
    {
        public ArkhenContext(DbContextOptions<ArkhenContext> options) :
            base(options) {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<InventoryEntry> InventoryEntries { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<LocationAdmin> LocationAdmins { get; set; }
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

                entity.HasOne(e => e.Address)
                    .WithMany()
                    .HasForeignKey(e => e.AddressId);

                entity.HasOne(e => e.DefaultLocation)
                    .WithMany()
                    .HasForeignKey(e => e.DefaultLocationId)
                    .OnDelete(DeleteBehavior.ClientNoAction);
            });

            modelBuilder.Entity<InventoryEntry>(entity => {
                entity.ToTable("InventoryEntry");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever();

                entity.Property(e => e.Price)
                    .IsRequired();

                entity.HasOne(e => e.Product)
                    .WithMany()
                    .HasForeignKey(e => e.ProductId);

                entity.HasOne(e => e.Location)
                    .WithMany(l => l.InventoryEntries)
                    .HasForeignKey(e => e.LocationId);
            });

            modelBuilder.Entity<Location>(entity => {
                entity.ToTable("Location");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<LocationAdmin>(entity => {
                entity.ToTable("LocationAdmin");

                entity.HasKey(la => new { la.LocationId, la.AdminId });

                entity.HasOne(la => la.Location)
                    .WithMany(l => l.LocationAdmins)
                    .HasForeignKey(la => la.LocationId);

                entity.HasOne(la => la.Admin)
                    .WithMany(a => a.LocationAdmins)
                    .HasForeignKey(la => la.AdminId);
            });

            modelBuilder.Entity<Order>(entity => {
                entity.ToTable("Order");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever();

                entity.Property(e => e.PlacementDate)
                    .IsRequired();

                entity.HasOne(e => e.Customer)
                    .WithMany(c => c.Orders)
                    .HasForeignKey(e => e.CustomerId);

                entity.HasOne(e => e.Admin)
                    .WithMany()
                    .HasForeignKey(e => e.AdminId);

                entity.HasOne(e => e.Location)
                    .WithMany()
                    .HasForeignKey(e => e.LocationId)
                    .OnDelete(DeleteBehavior.ClientNoAction);
            });

            modelBuilder.Entity<OrderLine>(entity => {
                entity.ToTable("OrderLine");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever();

                entity.Property(e => e.Count)
                    .IsRequired();

                entity.Property(e => e.PricePerUnit)
                    .IsRequired();

                entity.HasOne(e => e.Order)
                    .WithMany(o => o.OrderLines)
                    .HasForeignKey(e => e.OrderId);

                entity.HasOne(e => e.Product)
                    .WithMany()
                    .HasForeignKey(e => e.ProductId);
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
