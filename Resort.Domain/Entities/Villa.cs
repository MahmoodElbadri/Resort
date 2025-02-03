using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resort.Domain.Entities
{
    public class Villa
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public int Capacity { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public int Occupancy { get; set; }
        public int Sqft { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
