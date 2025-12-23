using Fourth_MVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fourth_MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<EmployeeRegister> Employees { get; set; }
        public DbSet<Complex> Complexes { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Land> Lands { get; set; }
        public DbSet<Aqarat> Aqarats { get; set; }
        public DbSet<Asset> Asset { get; set; } = default!;
        //public DbSet<Asset> Assets { get; set; }

    }
}


