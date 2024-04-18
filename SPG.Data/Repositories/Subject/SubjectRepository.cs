using SPG.Domain.Interfaces;
using SPG.Domain.Model;
using SPG.Data.Properties;

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
        _context.Subjects.Update(subject);
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
