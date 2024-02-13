using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace SPG.Intf.Model
{
    public class PersonModel
    {
        [Key]
        public  int Id { get; set; }

        [Required]
        [StringLength(45)]
        public string Cpf { get; set; } = string.Empty;
        
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Ucode { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public DateOnly BirthDate { get; set; } 

    }
}
