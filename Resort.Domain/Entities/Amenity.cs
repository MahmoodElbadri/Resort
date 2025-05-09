﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resort.Domain.Entities;

public class Amenity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } 
    [Required]
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int VillaId { get; set; }
    [ForeignKey(nameof(VillaId))]
    public Villa Villa { get; set; }
}
