using SPG.Domain.Interfaces;
using SPG.Domain.Model;
using SPG.Data.Properties;
using Microsoft.EntityFrameworkCore;

namespace SPG.Data.Repositories
{
  public class SystemParamsRepository(AppDbContext context) : ISystemParamsRepository
  {
    private readonly AppDbContext _context = context;

    public IEnumerable<SystemParamsModel> GetAll()
    {
      return _context.SystemParams.ToList();
    }

    public SystemParamsModel GetById(string id)
    {
      var systemParam = _context.SystemParams.FirstOrDefault(p => p.Id == id);

      return systemParam ?? throw new Exception(string.Format(Resources.NotFoundSystemParams, id));
    }

    public void Add(SystemParamsModel systemParam)
    {
      _context.SystemParams.Add(systemParam);
      _context.SaveChanges();
    }

    public void Update(SystemParamsModel systemParam)
    {
      try
      {
        var model = GetById(systemParam.Id);
        model.Integer = systemParam.Integer;
        model.String = systemParam.String;
        model.Double = systemParam.Double;
        model.Boolean = systemParam.Boolean;

        _context.Entry(model).State = EntityState.Modified;
        _context.SaveChanges();
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }

    public void Delete(string id)
    {
      var systemParam = _context.SystemParams.FirstOrDefault(p => p.Id == id);
      if (systemParam != null)
      {
        _context.SystemParams.Remove(systemParam);
        _context.SaveChanges();
      }
    }
  }
}
