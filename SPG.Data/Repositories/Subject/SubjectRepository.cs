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
      return _context.Subjects
        .Include(c => c.Teacher)
        .Include(c => c.Curriculum)
        .ToList();
    }

    public SubjectModel GetById(int id)
    {
      var subject = _context.Subjects
        .Include(c => c.Teacher)
        .Include(c => c.Curriculum)
        .FirstOrDefault(p => p.Id == id);

      return subject ?? throw new Exception(string.Format(Resources.NotFoundSubject, id));
    }

    public void Add(SubjectModel subject)
    {
      _context.Subjects.Add(subject);
      _context.SaveChanges();
      subject.Id = _context.Subjects.OrderByDescending(c => c.Id).Select(c => c.Id).FirstOrDefault();
    }

    public void AddAll(IList<SubjectModel> subjects)
    {
      _context.Subjects.AddRange(subjects);
      _context.SaveChanges();

      var addedIds = _context.Subjects.OrderByDescending(c => c.Id).Select(c => c.Id).ToList();

      for (int i = 0; i < subjects.Count; i++)
        subjects[i].Id = addedIds[i];
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
        model.Syllabus = subject.Syllabus;

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
