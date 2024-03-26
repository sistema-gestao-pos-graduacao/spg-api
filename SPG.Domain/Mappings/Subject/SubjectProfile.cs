using AutoMapper;
using SPG.Domain.Dto;
using SPG.Domain.Model;

namespace SPG.Domain.Mappings
{
    public class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateMap<SubjectModel, SubjectDto>();
        }
    }
}
