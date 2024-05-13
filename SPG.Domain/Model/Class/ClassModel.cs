﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPG.Domain.Model
{
  public class ClassModel()
  {
    [Key]
    public int Id { get; set; }

    [ForeignKey("Subject")]
    public int SubjectId { get; set; }

    public SubjectModel? Subject { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [StringLength(255)]
    public string Location { get; set; } = string.Empty;

    public int? Building { get; set; }

    public int? Room { get; set; }

    public List<string> Students { get; set; } = [];
  }
}
