using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReserveIt_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace ReserveIt_Backend
{
    public class UsersDatabaseContext : DbContext
    {
        public UsersDatabaseContext(DbContextOptions<UsersDatabaseContext> options)
            : base(options)
        { }
        public DbSet<User> Users { get; set; }
    }
}
