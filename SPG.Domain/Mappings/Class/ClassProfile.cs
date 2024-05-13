using AutoMapper;
using SPG.Domain.Dto;
using SPG.Domain.Model;

namespace SPG.Domain.Mappings
{
    public class ClassProfile : Profile
    {
        public ClassProfile()
        {
            CreateMap<ClassModel, ClassDto>()
              .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Curriculum != null ? src.Curriculum.Name : string.Empty))
              .ReverseMap()
              .ForMember(dest => dest.Curriculum, opt => opt.Ignore()); ;
        }
    }
}
