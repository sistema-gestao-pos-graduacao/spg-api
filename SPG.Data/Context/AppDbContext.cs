using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SPG.Domain.Model;

namespace SPG.Data.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<PersonModel> Persons { get; set; }

        public DbSet<UserModel> Users { get; set; }

        public DbSet<IdentityRole> IdentityRoles { get; set; }
    }
}
