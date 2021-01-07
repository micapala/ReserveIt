using ReserveIt_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace ReserveIt_Backend
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        { }
        public DbSet<User> Users { get; set; }

        public DbSet<Band> Bands { get; set; }

        public DbSet<Concert> Concerts { get; set; }

        public DbSet<Reservation> Reservations { get; set; }
    }
}
