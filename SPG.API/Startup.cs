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
      var baseDomain = Configuration["BaseDomain"];
      if (string.IsNullOrEmpty(baseDomain))
        throw new Exception("Base Domain cannot be empty");

      services.AddDbContext<AppDbContext>(options =>
          options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("SPG.Data")));

      services.AddIdentity<UserModel, IdentityRole>()
          .AddEntityFrameworkStores<AppDbContext>()
          .AddDefaultTokenProviders();

      services.AddCors(options =>
      {
        options.AddPolicy("AllowFrontend", builder =>
        {
          builder.WithOrigins(baseDomain)
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
        });
      });
      services.AddControllers();
      services.AddEndpointsApiExplorer();
      services.AddSwaggerGen();
      services.ConfigureApplicationCookie(options =>
      {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.Cookie.HttpOnly = true;
        options.Cookie.Domain = new Uri(baseDomain).Host; 
        options.Cookie.Path = "/"; 
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.Cookie.SameSite = SameSiteMode.None;
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
      
      app.UseCors("AllowFrontend");

      app.UseRouting();

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
