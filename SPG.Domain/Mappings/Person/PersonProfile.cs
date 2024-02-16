using AutoMapper;
using SPG.Domain.Dto.Person;
using SPG.Domain.Model.Person;

namespace SPG.Domain.Mappings.Person
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<PersonModel, PersonDto>();
        }
    }
}
