using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recyclable.Models
{
    public class RecyclableType
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Type name cannot exceed 100 characters.")]
        public string Type { get; set; }

        [Required]
        public decimal Rate { get; set; }

        [Required]
        public decimal MinKg { get; set; }

        [Required]
        public decimal MaxKg { get; set; }
    }
}