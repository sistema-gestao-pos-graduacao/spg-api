using AutoMapper;
using SPG.Domain.Dto;
using SPG.Domain.Model;

namespace SPG.Domain.Mappings
{
  public class TeacherAvailabilityProfile : Profile
  {
    public TeacherAvailabilityProfile()
    {
      CreateMap<TeacherAvailabilityModel, TeacherAvailabilityDto>()
        .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Teacher != null ? src.Teacher.Name : string.Empty))
        .ReverseMap()
        .ForMember(dest => dest.Teacher, opt => opt.Ignore());
    }
  }
}
