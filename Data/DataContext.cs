using Microsoft.EntityFrameworkCore;
using P4._0_backend.Models;


namespace P4._0_backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Parking> Parking { get; set; }
        public DbSet<Passing_Cars> Passing_Cars { get; set; }
        public DbSet<Users> Users { get; set; }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parking>().ToTable("Parking");
            modelBuilder.Entity<Passing_Cars>().ToTable("Passing_Cars");
            modelBuilder.Entity<Users>().ToTable("Users");
        }

    }
}
