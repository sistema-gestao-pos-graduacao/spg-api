using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SPG.Application.Person;
using SPG.Application.User;
using SPG.Data.Context;
using SPG.Data.Person;
using SPG.Domain.Interfaces;
using SPG.Domain.Mappings;
using SPG.Domain.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SPG.API
{
  public class Startup(IConfiguration configuration)
  {
    public readonly IConfiguration Configuration = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
      var connectionString = Configuration.GetConnectionString("DefaultConnection");
      services.AddDbContext<AppDbContext>(options => {
        options.UseSqlServer(connectionString, b => b.MigrationsAssembly("SPG.Data"));
      });

      services.AddControllers();
      services.AddEndpointsApiExplorer();
      services.AddSwaggerGen();
      services.AddIdentity<UserModel, IdentityRole>()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();
      services.ConfigureApplicationCookie(options =>
      {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
      });

      #region Mapper
      services.AddAutoMapper(typeof(PersonProfile));
      services.AddAutoMapper(typeof(UserProfile));
      #endregion

      #region Repositories
      services.AddScoped<IPersonRepository, PersonRepository>();
      #endregion

      #region Services
      services.AddScoped<IPersonService, PersonService>();
      services.AddScoped<IUserService, UserService>();
      #endregion
    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }
      SeedRoles(app).Wait();

      app.UseHttpsRedirection();

      app.UseAuthorization();

      app.MapControllers();
    }

    private static async Task SeedRoles(IApplicationBuilder app)
    {
      using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
      var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

      if (!await roleManager.RoleExistsAsync("Admin"))
      {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
      }

      if (!await roleManager.RoleExistsAsync("Professor"))
      {
        await roleManager.CreateAsync(new IdentityRole("Professor"));
      }

      if (!await roleManager.RoleExistsAsync("Coordinator"))
      {
        await roleManager.CreateAsync(new IdentityRole("Coordinator"));
      }

      if (!await roleManager.RoleExistsAsync("Student"))
      {
        await roleManager.CreateAsync(new IdentityRole("Student"));
      }
    }
  }
}
