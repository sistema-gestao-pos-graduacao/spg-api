﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPG.Domain.Model
{
  public class PersonModel()
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(45)]
    public string Cpf { get; set; } = string.Empty;

    [ForeignKey("User")]
    public string UserId { get; set; } = string.Empty;

    public IdentityUser? User { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public DateOnly BirthDate { get; set; }
  }
}
