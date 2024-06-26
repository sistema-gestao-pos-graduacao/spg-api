﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SPG.Application.Services;
using SPG.Data;
using SPG.Data.Repositories;
using SPG.Domain.Enums;
using SPG.Domain.Interfaces;
using SPG.Domain.Mappings;
using SPG.Domain.Model;
using SPG.Domain.SystemParams;
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
          builder.WithOrigins("http://localhost:3000",
            "https://localhost:3000", 
            "http://app-i575ajhit22gu.azurewebsites.net", 
            "https://app-i575ajhit22gu.azurewebsites.net",
            "http://delightful-moss-07baede1e.5.azurestaticapps.net",
            "https://delightful-moss-07baede1e.5.azurestaticapps.net",
            "https://happy-glacier-0cb37a70f.5.azurestaticapps.net")
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
        options.Cookie.Path = "/"; 
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.None;
        options.Events.OnSignedIn = context =>
        {
          options.Cookie.Domain = new Uri(context.Request.Scheme + "://" + context.Request.Host.Value).Host;
          return Task.CompletedTask;
        };
      });

      #region Mapper
      services.AddAutoMapper(typeof(PersonProfile));
      services.AddAutoMapper(typeof(UserProfile));
      services.AddAutoMapper(typeof(SubjectProfile));
      services.AddAutoMapper(typeof(TeacherAvailabilityProfile));
      services.AddAutoMapper(typeof(CourseProfile));
      services.AddAutoMapper(typeof(CurriculumProfile));
      services.AddAutoMapper(typeof(ClassScheduleProfile));
      services.AddAutoMapper(typeof(SystemParamsProfile));
      services.AddAutoMapper(typeof(ClassProfile));
      #endregion

      #region Repositories
      services.AddScoped<IPersonRepository, PersonRepository>();
      services.AddScoped<ISubjectRepository, SubjectRepository>();
      services.AddScoped<ITeacherAvailabilityRepository, TeacherAvailabilityRepository>();
      services.AddScoped<ICourseRepository, CourseRepository>();
      services.AddScoped<ICurriculumRepository, CurriculumRepository>();
      services.AddScoped<IClassScheduleRepository, ClassScheduleRepository>();
      services.AddScoped<ISystemParamsRepository, SystemParamsRepository>();
      services.AddScoped<IClassRepository, ClassRepository>();
      #endregion

      #region Services
      services.AddScoped<IPersonService, PersonService>();
      services.AddScoped<IUserService, UserService>();
      services.AddScoped<ILoginService, LoginService>();
      services.AddScoped<ISubjectService, SubjectService>();
      services.AddScoped<ITeacherAvailabilityService, TeacherAvailabilityService>();
      services.AddScoped<IEmailService, EmailService>();
      services.AddScoped<ICourseService, CourseService>();
      services.AddScoped<ICurriculumService, CurriculumService>();
      services.AddScoped<IClassScheduleService, ClassScheduleService>();
      services.AddScoped<ISystemParamsService, SystemParamsService>();
      services.AddScoped<IClassService, ClassService>();
      #endregion

      #region System Params
      services.AddScoped<SystemParams>();
      #endregion
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      app.UseSwagger();
      app.UseSwaggerUI();

      app.UseHttpsRedirection();
      app.UseCors("AllowFrontend");
      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
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
