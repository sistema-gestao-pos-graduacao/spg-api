using SPG.Domain.Model;

namespace SPG.Domain.Interfaces
{
    public interface ISystemParamsRepository
  {
        IEnumerable<SystemParamsModel> GetAll();
        SystemParamsModel GetById(string id);
        void Add(SystemParamsModel systemParam);
        void Update(SystemParamsModel systemParam);
        void Delete(string id);
    }
}
