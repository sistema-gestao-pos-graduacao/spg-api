﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SPG.Domain.Enums;

namespace SPG.Domain.Model
{
  public class AvailableTimeModel()
  {
    [Key]
    public int Id { get; set; }

    [ForeignKey("Person")]
    public int PersonId { get; set; }

    public PersonModel? Person { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [StringLength(200)]
    public string Time { get; set; } = string.Empty;
    
    [Required]
    public WeekDaysEnum WeekDays { get; set; }
    
  }
}
