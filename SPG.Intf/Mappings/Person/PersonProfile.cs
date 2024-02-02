using AutoMapper;
using SPG.Intf.Dto;
using SPG.Intf.Model;

namespace SPG.Intf.Mappings
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<PersonModel, PersonDto>();
        }
    }
}
