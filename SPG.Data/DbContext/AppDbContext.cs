using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SPG.Domain.Model;

namespace SPG.Data
{

  public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<UserModel, IdentityRole, string>(options)
  {
    public DbSet<PersonModel> Persons { get; set; }

    public DbSet<SubjectModel> Subjects { get; set; }

    public DbSet<TeacherAvailabilityModel> TeacherAvailabilities { get; set; }

    public DbSet<AvailableTimeModel> AvailableTimes { get; set; }

    public DbSet<ClassModel> Classes { get; set; }

    public DbSet<ExceptionDateModel> ExceptionDates { get; set; }

    public DbSet<ScheduledClassModel> ScheduledClasses { get; set; }

    public DbSet<SpecializationModel> Specializations { get; set; }

    public DbSet<CurriculumModel> Curriculums { get; set; }
  }
}

