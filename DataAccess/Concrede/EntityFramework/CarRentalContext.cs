using Core.Entities.Concrede;
using Entities.Concrede;
using Microsoft.EntityFrameworkCore;



namespace DataAccess.Concrede.EntityFramework
{
    public class CarRentalContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=CarRentalDB;Username=postgres;Password=carRental528;");
        }

        
        
        

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Customer> Customers { get; set; }  
        public DbSet<Rental> Rentals { get; set; }
        
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        
    }
}
