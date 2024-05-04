using SPG.Domain.Interfaces;
using SPG.Domain.Model;
using SPG.Data.Properties;
using Microsoft.EntityFrameworkCore;

namespace SPG.Data.Repositories
{
  public class ClassScheduleRepository(AppDbContext context) : IClassScheduleRepository
  {
    private readonly AppDbContext _context = context;

    public IEnumerable<ClassScheduleModel> GetAll()
    {
      return _context.ClassSchedules
        .Include(c => c.Teacher)
        .Include(c => c.Subject)
        .ToList();
    }

    public ClassScheduleModel GetById(int id)
    {
      var classSchedule = _context.ClassSchedules
        .Include(c => c.Teacher)
        .Include(c => c.Subject)
        .FirstOrDefault(p => p.Id == id);

      return classSchedule ?? throw new Exception(string.Format(Resources.NotFoundClassSchedule, id));
    }

    public void Add(ClassScheduleModel classSchedule)
    {
      _context.ClassSchedules.Add(classSchedule);
      _context.SaveChanges();
      classSchedule.Id = _context.ClassSchedules.OrderByDescending(c => c.Id).Select(c => c.Id).FirstOrDefault();
    }

    public void AddAll(IList<ClassScheduleModel> teacherAvailabilities)
    {
      _context.ClassSchedules.AddRange(teacherAvailabilities);
      _context.SaveChanges();

      var addedIds = _context.ClassSchedules.OrderByDescending(c => c.Id).Select(c => c.Id).ToList();

      for (int i = 0; i < teacherAvailabilities.Count; i++)
        teacherAvailabilities[i].Id = addedIds[i];
    }

    public void Update(ClassScheduleModel classSchedule)
    {
      try
      {
        var model = GetById(classSchedule.Id);
        model.TeacherId = classSchedule.TeacherId;
        model.SubjectId = classSchedule.SubjectId;
        model.StartDateTime = classSchedule.StartDateTime;
        model.EndDateTime = classSchedule.EndDateTime;

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
      var classSchedule = _context.ClassSchedules.FirstOrDefault(p => p.Id == id);
      if (classSchedule != null)
      {
        _context.ClassSchedules.Remove(classSchedule);
        _context.SaveChanges();
      }
    }
  }
}
