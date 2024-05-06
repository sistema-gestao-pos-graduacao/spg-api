using SPG.Domain.Interfaces;
using SPG.Domain.Model;
using SPG.Data.Properties;
using Microsoft.EntityFrameworkCore;

namespace SPG.Data.Repositories
{
  public class TeacherAvailabilityRepository(AppDbContext context) : ITeacherAvailabilityRepository
  {
    private readonly AppDbContext _context = context;

    public IEnumerable<TeacherAvailabilityModel> GetAll()
    {
      return _context.TeacherAvailabilities.Include(c => c.Teacher).ToList();
    }

    public TeacherAvailabilityModel GetById(int id)
    {
      var teacherAvailability = _context.TeacherAvailabilities.Include(c => c.Teacher).FirstOrDefault(p => p.Id == id);

      return teacherAvailability ?? throw new Exception(string.Format(Resources.NotFoundTeacherAvailability, id));
    }

    public void Add(TeacherAvailabilityModel teacherAvailability)
    {
      _context.TeacherAvailabilities.Add(teacherAvailability);
      _context.SaveChanges();
      teacherAvailability.Id = _context.TeacherAvailabilities.OrderByDescending(c => c.Id).Select(c => c.Id).FirstOrDefault();
    }

    public void AddAll(IList<TeacherAvailabilityModel> teacherAvailabilities)
    {
      _context.TeacherAvailabilities.AddRange(teacherAvailabilities);
      _context.SaveChanges();

      var addedIds = _context.TeacherAvailabilities.OrderByDescending(c => c.Id).Select(c => c.Id).ToList();

      for (int i = 0; i < teacherAvailabilities.Count; i++)
        teacherAvailabilities[i].Id = addedIds[i];
    }

    public void Update(TeacherAvailabilityModel teacherAvailability)
    {
      try
      {
        var model = GetById(teacherAvailability.Id);
        model.TeacherId = teacherAvailability.TeacherId;
        model.StartDateTime = teacherAvailability.StartDateTime;
        model.EndDateTime = teacherAvailability.EndDateTime;

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
      var teacherAvailability = _context.TeacherAvailabilities.FirstOrDefault(p => p.Id == id);
      if (teacherAvailability != null)
      {
        _context.TeacherAvailabilities.Remove(teacherAvailability);
        _context.SaveChanges();
      }
    }

    public void DeleteAll(List<int> ids)
    {
      var teacherAvailability = _context.TeacherAvailabilities.Where(c => ids.Contains(c.Id)).ToList();
      if (teacherAvailability != null)
      {
        _context.TeacherAvailabilities.RemoveRange(teacherAvailability);
        _context.SaveChanges();
      }
    }
  }
}
