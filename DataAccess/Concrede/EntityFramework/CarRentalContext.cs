﻿using Entities.Concrede;
using Microsoft.EntityFrameworkCore;



namespace DataAccess.Concrede.EntityFramework
{
    public class CarRentalContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=192.168.1.108;Port=5432;Database=CarRentalDB;Username=postgres;Password=carRental528;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity =>
            {
                entity.ToTable("cars");
                entity.HasKey(e => e.CarId);
                entity.Property(e => e.CarId).HasColumnName("id");
                entity.Property(e => e.BrandId).HasColumnName("brand_id");
                entity.Property(e => e.ColorId).HasColumnName("color_id");
                entity.Property(e => e.Model).HasColumnName("model");
                entity.Property(e => e.Year).HasColumnName("year");
                entity.Property(e => e.DailyPrice).HasColumnName("daily_price");
            });

            // Diğer entity'ler için de benzer şekilde yapacaksın:
            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("brands");
                entity.HasKey(e => e.BrandId);
                entity.Property(e => e.BrandId).HasColumnName("id");
                entity.Property(e => e.BrandName).HasColumnName("name");
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.ToTable("colors");
                entity.HasKey(e => e.ColorId);
                entity.Property(e => e.ColorId).HasColumnName("id");
                entity.Property(e => e.ColorName).HasColumnName("name");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customers");
                entity.HasKey(e => e.CustomerId);
                entity.Property(e => e.CustomerId).HasColumnName("id");
                entity.Property(e => e.FirstName).HasColumnName("first_name");
                entity.Property(e => e.LastName).HasColumnName("last_name");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.Country).HasColumnName("country");
                entity.Property(e => e.Phone).HasColumnName("phone");
            });

            modelBuilder.Entity<Rental>(entity =>
            {
                entity.ToTable("rentals");
                entity.HasKey(e => e.RentalId);
                entity.Property(e => e.RentalId).HasColumnName("id");
                entity.Property(e => e.CarId).HasColumnName("car_id");
                entity.Property(e => e.CustomerId).HasColumnName("customer_id");
                entity.Property(e => e.PickupDate).HasColumnName("pickup_date");
                entity.Property(e => e.ReturnDate).HasColumnName("return_date");
            });
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Customer> Customers { get; set; }  
        public DbSet<Rental> Rentals { get; set; }

    }
}
