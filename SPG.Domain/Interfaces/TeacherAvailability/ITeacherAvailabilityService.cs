using SPG.Domain.Dto;

namespace SPG.Domain.Interfaces
{
    public interface ITeacherAvailabilityService
  {
        IEnumerable<TeacherAvailabilityDto> GetAllTeacherAvailabilities();
        TeacherAvailabilityDto GetTeacherAvailabilityById(int id);
        TeacherAvailabilityDto AddTeacherAvailability(TeacherAvailabilityDto person);
        TeacherAvailabilityDto UpdateTeacherAvailability(TeacherAvailabilityDto person);
        void DeleteTeacherAvailability(int id);
    }
}
