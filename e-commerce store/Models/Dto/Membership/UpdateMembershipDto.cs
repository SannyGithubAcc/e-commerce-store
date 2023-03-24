using e_commerce_store.Models.Dto.Order;
using System.ComponentModel.DataAnnotations;

namespace e_commerce_store.Dto.Membership
{
    public class UpdateMembershipDto
    {
        public int Id { get; set; }
        [Display(Name = "Is Active", Description = "Indicates whether the object is active or not")]
        public bool IsActive { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(4000)]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
    
    }
}
