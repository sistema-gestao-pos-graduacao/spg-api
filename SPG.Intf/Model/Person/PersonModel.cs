using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPG.Intf.Model
{
    public class PersonModel
    {
        [Key]
        public  int Id { get; set; }

        [Key]
        public string Cpf { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public int UserId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public DateOnly BirthDate { get; set; } 

    }
}
