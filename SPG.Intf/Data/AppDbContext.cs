using Microsoft.EntityFrameworkCore;
using SPG.Intf.Model;

namespace SPG.Intf.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<PersonModel> Persons { get; set; }
    }
}
