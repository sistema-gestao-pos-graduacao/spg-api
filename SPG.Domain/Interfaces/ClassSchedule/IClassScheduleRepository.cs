using SPG.Domain.Model;

namespace SPG.Domain.Interfaces
{
    public interface IClassScheduleRepository
  {
        IEnumerable<ClassScheduleModel> GetAll();
        ClassScheduleModel GetById(int id);
        void Add(ClassScheduleModel classSchedule);
        void AddAll(IList<ClassScheduleModel> classSchedules);
        void Update(ClassScheduleModel classSchedule);
        void Delete(int id);
    }
}
