using AutoMapper;
using SPG.Domain.Dto;
using SPG.Domain.Model;

namespace SPG.Domain.Mappings
{
    public class CurriculumProfile : Profile
    {
        public CurriculumProfile()
        {
            CreateMap<CurriculumModel, CurriculumDto>()
              .ForMember(dest => dest.Course, opt => opt.MapFrom(src => src.Course != null ? src.Course.Name : string.Empty))
              .ReverseMap();
        }
    }
}
