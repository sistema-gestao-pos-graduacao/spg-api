using SPG.Domain.Interfaces;
using SPG.Domain.Model;
using SPG.Data.Properties;

namespace SPG.Data.Repositories
{
  public class CurriculumRepository(AppDbContext context) : ICurriculumRepository
  {
    private readonly AppDbContext _context = context;

    public IEnumerable<CurriculumModel> GetAll()
    {
      return _context.Curriculums.ToList();
    }

    public CurriculumModel GetById(int id)
    {
      var subject = _context.Curriculums.FirstOrDefault(p => p.Id == id);

      return subject ?? throw new Exception(string.Format(Resources.NotFoundCurriculum, id));
    }

    public void Add(CurriculumModel subject)
    {
      _context.Curriculums.Add(subject);
      subject.Id = _context.SaveChanges();
    }

    public void Update(CurriculumModel subject)
    {
      try
      {
        _context.Curriculums.Update(subject);
        _context.SaveChanges();
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }

    public void Delete(int id)
    {
      var subject = _context.Curriculums.FirstOrDefault(p => p.Id == id);
      if (subject != null)
      {
        _context.Curriculums.Remove(subject);
        _context.SaveChanges();
      }
    }
  }
}
