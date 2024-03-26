using SPG.Data.Context;
using SPG.Domain.Interfaces;
using SPG.Domain.Model;
using SPG.Data.Properties;

namespace SPG.Data.Repositories
{
  public class TeacherAvailabilityRepository(AppDbContext context) : ITeacherAvailabilityRepository
  {
    private readonly AppDbContext _context = context;

    public IEnumerable<TeacherAvailabilityModel> GetAll()
    {
      return _context.TeacherAvailabilities.ToList();
    }

    public TeacherAvailabilityModel GetById(int id)
    {
      var subject = _context.TeacherAvailabilities.FirstOrDefault(p => p.Id == id);

      return subject ?? throw new Exception(string.Format(Resources.NotFoundTeacherAvailability, id));
    }

    public void Add(TeacherAvailabilityModel subject)
    {
      _context.TeacherAvailabilities.Add(subject);
      subject.Id = _context.SaveChanges();
    }

    public void Update(TeacherAvailabilityModel subject)
    {
      try
      {
        _context.TeacherAvailabilities.Update(subject);
        _context.SaveChanges();
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }

    public void Delete(int id)
    {
      var subject = _context.TeacherAvailabilities.FirstOrDefault(p => p.Id == id);
      if (subject != null)
      {
        _context.TeacherAvailabilities.Remove(subject);
        _context.SaveChanges();
      }
    }
  }
}
