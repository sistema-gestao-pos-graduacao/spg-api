namespace SPG.Domain.Dto.Person
{
    public class PersonDto
    {
        public required string Cpf { get; set; }
        public int UserId { get; set; }
        public Guid Ucode { get; set; }
        public required string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
