using ReserveIt_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace ReserveIt_Backend
{
    public class ApiContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseInMemoryDatabase(databaseName: "ReserveIt");

        public DbSet<User> Users { get; set; }

        public DbSet<Band> Bands { get; set; }

        public DbSet<Concert> Concerts { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Concert>()
                .HasOne(b => b.Band)
                .WithOne();
        }
    }
}
