﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resort.Domain.Entities;

public class VillaNumber
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [DisplayName("Villa Number")]
    public int Villa_Number { get; set; }
    [ForeignKey("Villa")]
    [DisplayName("Villa Id")]
    public int VillaId { get; set; }
    [ValidateNever]
    public Villa? Villa { get; set; }
    [DisplayName("Special Details")]
    public string? SpecialDetails { get; set; }
}
