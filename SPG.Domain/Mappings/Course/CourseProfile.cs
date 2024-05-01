using AutoMapper;
using SPG.Domain.Dto;
using SPG.Domain.Model;

namespace SPG.Domain.Mappings
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<CourseModel, CourseDto>()
              .ForMember(dest => dest.Coordinator, opt => opt.MapFrom(src => src.Coordinator != null ? src.Coordinator.Name : string.Empty))
              .ReverseMap()
              .ForMember(dest => dest.Coordinator, opt => opt.MapFrom(src => new PersonModel()));
        }
    }
}
