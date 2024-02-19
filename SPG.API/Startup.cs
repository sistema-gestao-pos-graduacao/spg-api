using Microsoft.EntityFrameworkCore;
using SPG.Application.Person;
using SPG.Data.Context;
using SPG.Data.Person;
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
            #endregion

            #region Services
            services.AddScoped<IPersonService, PersonService>();
            #endregion
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
