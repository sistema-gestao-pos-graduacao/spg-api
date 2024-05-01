using SPG.Domain.Interfaces;
using SPG.Domain.Model;
using SPG.Data.Properties;
using Microsoft.EntityFrameworkCore;

namespace SPG.Data.Repositories
{
  public class SubjectRepository(AppDbContext context) : ISubjectRepository
  {
    private readonly AppDbContext _context = context;

    public IEnumerable<SubjectModel> GetAll()
    {
      return _context.Subjects.ToList();
    }

    public SubjectModel GetById(int id)
    {
      var subject = _context.Subjects.FirstOrDefault(p => p.Id == id);

      return subject ?? throw new Exception(string.Format(Resources.NotFoundSubject, id));
    }

    public void Add(SubjectModel subject)
    {
      _context.Subjects.Add(subject);
      subject.Id = _context.SaveChanges();
    }

    public void Update(SubjectModel subject)
    {
      try
      {
        var model = GetById(subject.Id);
        model.WeekDay = subject.WeekDay;
        model.TeacherId = subject.TeacherId;  
        model.CurriculumId = subject.CurriculumId;
        model.Room = subject.Room;  
        model.Building =  subject.Building;
        model.Considerations = subject.Considerations;
        model.Location = subject.Location;
        model.Students = subject.Students;
        model.Hours = subject.Hours;

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
      var subject = _context.Subjects.FirstOrDefault(p => p.Id == id);
      if (subject != null)
      {
        _context.Subjects.Remove(subject);
        _context.SaveChanges();
      }
    }
  }
}
