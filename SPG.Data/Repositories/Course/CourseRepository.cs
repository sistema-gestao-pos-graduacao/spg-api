using SPG.Domain.Interfaces;
using SPG.Domain.Model;
using SPG.Data.Properties;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace SPG.Data.Repositories
{
  public class CourseRepository(AppDbContext context) : ICourseRepository
  {
    private readonly AppDbContext _context = context;

    public IEnumerable<CourseModel> GetAll()
    {
      return _context.Courses.Include(c => c.Coordinator).ToList();
    }

    public CourseModel GetById(int id)
    {
      var course = _context.Courses.Include(c => c.Coordinator).FirstOrDefault(p => p.Id == id);

      return course ?? throw new Exception(string.Format(Resources.NotFoundCourse, id));
    }

    public void Add(CourseModel course)
    {
      string sql = @"
        INSERT INTO COURSES (Name, CoordinatorId)
        VALUES (@CourseName, @CoordinatorId);
        SELECT SCOPE_IDENTITY();
      ";

      _context.Database.ExecuteSqlRaw(sql,
          new SqlParameter("@CourseName", course.Name),
          new SqlParameter("@CoordinatorId", (object?)course.CoordinatorId ?? DBNull.Value));

      course.Id = context.Courses.OrderByDescending(c => c.Id).Select(c => c.Id).FirstOrDefault();
    }


  public void Update(CourseModel course)
    {
      try
      {
        var model = GetById(course.Id);
        model.CoordinatorId = course.CoordinatorId;
        model.Name = course.Name;

        _context.Entry(model).State = EntityState.Modified;
        _context.SaveChanges();
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }

    public void Delete(int id)
    {
      var course = _context.Courses.FirstOrDefault(p => p.Id == id);
      if (course != null)
      {
        _context.Courses.Remove(course);
        _context.SaveChanges();
      }
    }
  }
}
