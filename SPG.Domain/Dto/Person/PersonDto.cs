using SPG.Domain.Enums;

namespace SPG.Domain.Dto
{
    public class PersonDto
    {
        public required string Cpf { get; set; }
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public PersonTypeEnum PersonType { get; set; }
    }
}
