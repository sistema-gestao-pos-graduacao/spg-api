using SPG.Domain.Interfaces;
using SPG.Domain.Model;
using SPG.Data.Properties;

namespace SPG.Data.Repositories
{
  public class CourseRepository(AppDbContext context) : ICourseRepository
  {
    private readonly AppDbContext _context = context;

    public IEnumerable<CourseModel> GetAll()
    {
      return _context.Courses.ToList();
    }

    public CourseModel GetById(int id)
    {
      var subject = _context.Courses.FirstOrDefault(p => p.Id == id);

      return subject ?? throw new Exception(string.Format(Resources.NotFoundCourse, id));
    }

    public void Add(CourseModel subject)
    {
      _context.Courses.Add(subject);
      subject.Id = _context.SaveChanges();
    }

    public void Update(CourseModel subject)
    {
      try
      {
        _context.Courses.Update(subject);
        _context.SaveChanges();
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }

    public void Delete(int id)
    {
      var subject = _context.Courses.FirstOrDefault(p => p.Id == id);
      if (subject != null)
      {
        _context.Courses.Remove(subject);
        _context.SaveChanges();
      }
    }
  }
}
