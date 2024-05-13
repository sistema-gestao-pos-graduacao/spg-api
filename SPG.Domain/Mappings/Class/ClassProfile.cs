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
              .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject != null ? src.Subject.Name : string.Empty))
              .ReverseMap()
              .ForMember(dest => dest.Subject, opt => opt.Ignore()); ;
        }
    }
}
