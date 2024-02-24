using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SPG.Application.Authorization;
using SPG.Application.Person;
using SPG.Application.User;
using SPG.Data.Context;
using SPG.Data.Person;
using SPG.Data.User;
using SPG.Domain.Interfaces;
using SPG.Domain.Mappings;

namespace SPG.API
{
  public class Startup(IConfiguration configuration)
  {
    public IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
      var connectionString = Configuration.GetConnectionString("DefaultConnection");
      services.AddDbContext<AppDbContext>(options => {
          options.UseSqlServer(connectionString);
      });

      services.AddControllers();
      services.AddEndpointsApiExplorer();
      services.AddSwaggerGen();

      #region Mapper
      services.AddAutoMapper(typeof(PersonProfile));
      services.AddAutoMapper(typeof(UserProfile));
      #endregion

      #region Repositories
      services.AddScoped<IPersonRepository, PersonRepository>();
      services.AddScoped<IUserRepository, UserRepository>();
      services.AddScoped<IAuthorizationRepository, AuthorizationRepository>();
      #endregion

      #region Services
      services.AddScoped<IPersonService, PersonService>();
      services.AddScoped<IUserService, UserService>();
      services.AddScoped<IAuthorizationService, AuthorizationService>();
      #endregion
    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
      if (app.Environment.IsDevelopment())
      {
          app.UseSwagger();
          app.UseSwaggerUI();
      }
      app.UseGenerateJwtSecret();

      app.UseHttpsRedirection();

      app.UseAuthorization();

      app.MapControllers();
    }
  }
}
