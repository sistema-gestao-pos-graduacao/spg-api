using Microsoft.EntityFrameworkCore;
using SPG.Domain.Model.Person;

namespace SPG.Data.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<PersonModel> Persons { get; set; }
    }
}
