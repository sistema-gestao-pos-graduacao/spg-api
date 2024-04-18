using SPG.Domain.Enums;

namespace SPG.Domain.Dto
{
    public class PersonDto
    {
        public string Cpf { get; set; } = string.Empty;
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public required string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public PersonTypeEnum? PersonType { get; set; }
        public required string Email { get; set; }
    }
}
