using AutoMapper;
using SPG.Domain.Dto;
using SPG.Domain.Model;

namespace SPG.Domain.Mappings
{
  public class TeacherAvailabilityProfile : Profile
  {
    public TeacherAvailabilityProfile()
    {
      CreateMap<TeacherAvailabilityModel, TeacherAvailabilityDto>();
    }
  }
}
