using AutoMapper;
using SPG.Domain.Dto;
using SPG.Domain.Model;

namespace SPG.Domain.Mappings
{
    public class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateMap<SubjectModel, SubjectDto>()
              .ForMember(dest => dest.CurriculumName, opt => opt.MapFrom(src => src.Curriculum != null ? src.Curriculum.Name : string.Empty))
              .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Teacher != null ? src.Teacher.Name : string.Empty))
              .ReverseMap();
        }
    }
}
