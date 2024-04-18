using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SPG.Application.Services;
using SPG.Data;
using SPG.Data.Repositories;
using SPG.Domain.Enums;
using SPG.Domain.Interfaces;
using SPG.Domain.Mappings;
using SPG.Domain.Model;
using System;

namespace SPG.API
{
  public class Startup(IConfiguration configuration)
  {
    public readonly IConfiguration Configuration = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("SPG.Data")));
     
      var baseDomain = Configuration["BaseDomain"];
      if (string.IsNullOrEmpty(baseDomain))
        throw new Exception("Base Domain cannot be empty");

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
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
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
      services.AddScoped<IEmailService, EmailService>();
      #endregion
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }

      app.UseHttpsRedirection();
      app.UseCors("AllowFrontend");
      app.UseRouting();
      
      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
     
      SeedRoles(app).Wait();
    }

    private static async Task SeedRoles(IApplicationBuilder app)
    {
      using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
      var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

      if (!await roleManager.RoleExistsAsync(Enum.GetName(typeof(PersonTypeEnum), PersonTypeEnum.Admin) ?? "Admin"))
      {
        await roleManager.CreateAsync(new IdentityRole(Enum.GetName(typeof(PersonTypeEnum), PersonTypeEnum.Admin) ?? "Admin"));
      }

      if (!await roleManager.RoleExistsAsync(Enum.GetName(typeof(PersonTypeEnum), PersonTypeEnum.Teacher) ?? "Teacher"))
      {
        await roleManager.CreateAsync(new IdentityRole(Enum.GetName(typeof(PersonTypeEnum), PersonTypeEnum.Teacher) ?? "Teacher"));
      }

      if (!await roleManager.RoleExistsAsync(Enum.GetName(typeof(PersonTypeEnum), PersonTypeEnum.Coordinator) ?? "Coordinator"))
      {
        await roleManager.CreateAsync(new IdentityRole(Enum.GetName(typeof(PersonTypeEnum), PersonTypeEnum.Coordinator) ?? "Coordinator"));
      }
    }
  }
}
