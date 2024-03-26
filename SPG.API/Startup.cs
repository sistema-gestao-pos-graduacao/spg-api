using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SPG.Application.Services;
using SPG.Data.Context;
using SPG.Data.Repositories;
using SPG.Domain.Interfaces;
using SPG.Domain.Mappings;
using SPG.Domain.Model;

namespace SPG.API
{
  public class Startup(IConfiguration configuration)
  {
    public readonly IConfiguration Configuration = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<AppDbContext>(options =>
          options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("SPG.Data")));

      services.AddIdentity<UserModel, IdentityRole>()
          .AddEntityFrameworkStores<AppDbContext>()
          .AddDefaultTokenProviders();

      services.AddControllers();
      services.AddEndpointsApiExplorer();
      services.AddSwaggerGen();
      services.ConfigureApplicationCookie(options =>
      {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
      });

      #region Mapper
      services.AddAutoMapper(typeof(PersonProfile));
      services.AddAutoMapper(typeof(UserProfile));
      services.AddAutoMapper(typeof(SubjectProfile));
      services.AddAutoMapper(typeof(TeacherAvailabilityProfile));
      #endregion

      #region Repositories
      services.AddScoped<IPersonRepository, PersonRepository>();
      services.AddScoped<ISubjectRepository, SubjectRepository>();
      services.AddScoped<ITeacherAvailabilityRepository, TeacherAvailabilityRepository>();
      #endregion

      #region Services
      services.AddScoped<IPersonService, PersonService>();
      services.AddScoped<IUserService, UserService>();
      services.AddScoped<ILoginService, LoginService>();
      services.AddScoped<ISubjectService, SubjectService>();
      services.AddScoped<ITeacherAvailabilityService, TeacherAvailabilityService>();
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

      if (!await roleManager.RoleExistsAsync("Teacher"))
      {
        await roleManager.CreateAsync(new IdentityRole("Teacher"));
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
