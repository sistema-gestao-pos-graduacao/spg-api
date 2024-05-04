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

    public DbSet<ClassScheduleModel> ClassSchedules { get; set; }

    public DbSet<SystemParamsModel> SystemParams { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<PersonModel>()
          .HasOne(p => p.User)
          .WithOne()
          .HasForeignKey<PersonModel>(p => p.UserId)
          .OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<CourseModel>()
          .HasOne(c => c.Coordinator)
          .WithMany()
          .HasForeignKey(c => c.CoordinatorId)
          .OnDelete(DeleteBehavior.SetNull);

      modelBuilder.Entity<ClassScheduleModel>()
          .HasOne(c => c.Teacher)
          .WithMany()
          .HasForeignKey(c => c.TeacherId)
          .OnDelete(DeleteBehavior.NoAction);

      modelBuilder.Entity<ClassScheduleModel>()
          .HasOne(c => c.Subject)
          .WithMany()
          .HasForeignKey(c => c.SubjectId)
          .OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<CurriculumModel>()
          .HasOne(c => c.Course)
          .WithMany()
          .HasForeignKey(c => c.CourseId)
          .OnDelete(DeleteBehavior.Cascade);
    }
  }
}

