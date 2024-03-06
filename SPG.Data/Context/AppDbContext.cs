using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SPG.Domain.Model;

namespace SPG.Data.Context
{
  public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<UserModel, IdentityRole, string>(options)
  {
    public DbSet<PersonModel> Persons { get; set; }
  }
}

