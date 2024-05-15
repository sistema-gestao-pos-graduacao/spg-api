using SPG.Domain.Dto;

namespace SPG.Domain.Interfaces
{
    public interface IClassScheduleService
  {
        IEnumerable<ClassScheduleDto> GetAllClassSchedules();
        IEnumerable<ClassScheduleDto> GetAllFilteredByClass(int id);
        ClassScheduleDto GetClassScheduleById(int id);
        ClassScheduleDto AddClassSchedule(ClassScheduleDto classSchedule);
        ClassScheduleDto UpdateClassSchedule(ClassScheduleDto classSchedule);
        IList<ClassScheduleDto> AddClassSchedules(List<ClassScheduleDto> ClassSchedules);
        void DeleteClassSchedule(int id);
        void DeleteClassSchedules(List<int> ids);
        void RemoveClassIdFromRelatedClasses(int classId);
    }
}
