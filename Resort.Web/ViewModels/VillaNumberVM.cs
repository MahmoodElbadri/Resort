﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Resort.Domain.Entities;

namespace Resort.Web.ViewModels;

public class VillaNumberVM
{
    public VillaNumber? VillaNumber { get; set; } = new VillaNumber();      
    [ValidateNever]
    public IEnumerable<SelectListItem>? VillaList { get; set; }
}
