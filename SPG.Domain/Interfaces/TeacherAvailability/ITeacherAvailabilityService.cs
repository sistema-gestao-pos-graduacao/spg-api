using SPG.Domain.Dto;

namespace SPG.Domain.Interfaces
{
    public interface ITeacherAvailabilityService
  {
        IEnumerable<TeacherAvailabilityDto> GetAllTeacherAvailabilities();
        TeacherAvailabilityDto GetTeacherAvailabilityById(int id);
        TeacherAvailabilityDto AddTeacherAvailability(TeacherAvailabilityDto teacherAvailability);
        IList<TeacherAvailabilityDto> AddTeacherAvailabilities(List<TeacherAvailabilityDto> teacherAvailabilities);
        TeacherAvailabilityDto UpdateTeacherAvailability(TeacherAvailabilityDto teacherAvailability);
        void DeleteTeacherAvailability(int id);
        void DeleteTeacherAvailabilities(List<int> ids);
    }
}
