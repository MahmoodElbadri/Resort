using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resort.Domain.Entities
{
    public class Villa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        [Required]
        [MaxLength(30)]
        [DisplayName("Villa Name")]
        public string? Name { get; set; }
        public int Capacity { get; set; }
        [DisplayName("Price per Night")]
        [Range(10,10000)]
        public double Price { get; set; }
        public string? Description { get; set; }
        [Range(1,10)]
        public int Occupancy { get; set; }
        public int Sqft { get; set; }
        [DisplayName("Image URL")]
        public string? ImageUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
    }
}
