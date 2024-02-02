namespace SPG.Intf.Dto
{
    public class PersonDto
    {
        public int Id { get; set; }
        public required string Cpf { get; set; }
        public int UserId { get; set; }
        public required string Name { get; set; }
        public DateOnly BirthDate { get; set; }
    }
}
