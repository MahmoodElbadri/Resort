using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resort.Domain.Entities
{
    public class VillaNumber
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//it's your responsibility to make sure that the villa number is unique
        //by changing it to identity it will be unique for each villa and it's the responsibility of the database
        public int VillaNo { get; set; }
        [ForeignKey("Villa")]
        public int VillaId { get; set; }
        [ValidateNever]
        public virtual Villa Villa { get; set; }
        public string? SpecialDetails { get; set; }
    }
}
