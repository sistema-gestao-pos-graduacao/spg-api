using SPG.Domain.Interfaces;
using SPG.Domain.Model;
using SPG.Data.Properties;
using Microsoft.EntityFrameworkCore;

namespace SPG.Data.Repositories
{
  public class CurriculumRepository(AppDbContext context) : ICurriculumRepository
  {
    private readonly AppDbContext _context = context;

    public IEnumerable<CurriculumModel> GetAll()
    {
      return _context.Curriculums.Include(c => c.Course).ToList();
    }

    public CurriculumModel GetById(int id)
    {
      var curriculum = _context.Curriculums.Include(c => c.Course).FirstOrDefault(p => p.Id == id);

      return curriculum ?? throw new Exception(string.Format(Resources.NotFoundCurriculum, id));
    }

    public void Add(CurriculumModel curriculum)
    {
      _context.Curriculums.Add(curriculum);
      _context.SaveChanges();
      curriculum.Id = _context.Curriculums.OrderByDescending(c => c.Id).Select(c => c.Id).FirstOrDefault();
    }

    public void Update(CurriculumModel curriculum)
    {
      try
      {
        var model = GetById(curriculum.Id);
        model.Name = curriculum.Name;
        model.CourseId = curriculum.CourseId;

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
      var curriculum = _context.Curriculums.FirstOrDefault(p => p.Id == id);
      if (curriculum != null)
      {
        _context.Curriculums.Remove(curriculum);
        _context.SaveChanges();
      }
    }
  }
}
