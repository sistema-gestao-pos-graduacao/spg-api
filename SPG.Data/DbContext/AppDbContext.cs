using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SPG.Domain.Model;
using System;

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

    public DbSet<CourseModel> Courses { get; set; }

    public DbSet<CurriculumModel> Curriculums { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<PersonModel>()
          .HasOne(p => p.User)
          .WithOne()
          .HasForeignKey<PersonModel>(p => p.UserId)
          .OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<CourseModel>()
          .HasOne(p => p.Coordinator)
          .WithOne()
          .HasForeignKey<CourseModel>(p => p.CoordinatorId)
          .OnDelete(DeleteBehavior.SetNull);

      modelBuilder.Entity<CurriculumModel>()
          .HasOne(p => p.Course)
          .WithOne()
          .HasForeignKey<CurriculumModel>(p => p.CourseId)
          .OnDelete(DeleteBehavior.Cascade);
    }
  }
}

