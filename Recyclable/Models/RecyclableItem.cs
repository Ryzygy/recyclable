using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recyclable.Models
{
    public class RecyclableItem
    {
        public int Id { get; set; }

        [ForeignKey("RecyclableType")] 
        [Required]
        public int RecyclableTypeId { get; set; }

        [Required]
        public decimal Weight { get; set; }

        [DisplayName("Computed Rate")]
        public decimal ComputedRate { get; set; }

        [Required]
        [DisplayName("Description")]
        [StringLength(150, ErrorMessage = "Item description cannot exceed 150 characters.")]
        public string ItemDescription { get; set; }

        public virtual RecyclableType RecyclableType { get; set; }
    }
}
