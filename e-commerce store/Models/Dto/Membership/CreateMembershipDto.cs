using e_commerce_store.Models.Dto.Order;
using System.ComponentModel.DataAnnotations;

namespace e_commerce_store.Dto.Membership
{
    public class CreateMembershipDto
    {


        [Display(Name = "Is Active", Description = "Indicates whether the object is active or not")]
        public bool IsActive { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        // Navigation property


    }
}


