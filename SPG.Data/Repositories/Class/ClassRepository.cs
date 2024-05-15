using SPG.Domain.Interfaces;
using SPG.Domain.Model;
using SPG.Data.Properties;
using Microsoft.EntityFrameworkCore;

namespace SPG.Data.Repositories
{
  public class ClassRepository(AppDbContext context) : IClassRepository
  {
    private readonly AppDbContext _context = context;

    public IEnumerable<ClassModel> GetAll()
    {
      return _context.Classes.Include(c => c.Curriculum).ToList();
    }

    public ClassModel GetById(int id)
    {
      var classObj = _context.Classes.Include(c => c.Curriculum).FirstOrDefault(p => p.Id == id);

      return classObj ?? throw new Exception(string.Format(Resources.NotFoundClass, id));
    }

    public ClassModel? GetByName(string name)
    {
      var classObj = _context.Classes.Include(c => c.Curriculum).FirstOrDefault(p => p.Name == name);

      return classObj;
    }

    public void Add(ClassModel classObj)
    {
      _context.Classes.Add(classObj);
      _context.SaveChanges();
      classObj.Id = _context.Classes.OrderByDescending(c => c.Id).Select(c => c.Id).FirstOrDefault();
    }

    public void Update(ClassModel classObj)
    {
      try
      {
        var model = GetById(classObj.Id);
        model.CurriculumId = classObj.CurriculumId;
        model.Students = classObj.Students;

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
      var classObj = _context.Classes.FirstOrDefault(p => p.Id == id);
      if (classObj != null)
      {
        _context.Classes.Remove(classObj);
        _context.SaveChanges();
      }
    }
  }
}
