using AutoMapper;
using SPG.Domain.Dto;
using SPG.Domain.Model;

namespace SPG.Domain.Mappings
{
  public class ClassScheduleProfile : Profile
  {
    public ClassScheduleProfile()
    {
      CreateMap<ClassScheduleModel, ClassScheduleDto>()
        .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Teacher != null ? src.Teacher.Name : string.Empty))
        .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject != null ? src.Subject.Name : string.Empty))
        .ReverseMap()
        .ForMember(dest => dest.Teacher, opt => opt.Ignore())
        .ForMember(dest => dest.Subject, opt => opt.Ignore());
    }
  }
}
