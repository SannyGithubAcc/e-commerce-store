using System.ComponentModel.DataAnnotations;

namespace e_commerce_store.Dto
{
    public class CustomerCreateDto
    {
        [Required(ErrorMessage = "The Name field is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The Name field must be between 2 and 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Email field is required")]
        [EmailAddress(ErrorMessage = "The Email field must be a valid email address")]
        public string Email { get; set; }

        [Display(Name = "Is Active", Description = "Indicates whether the object is active or not")]
        public bool IsActive { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "The Phone field must be a 10-digit phone number")]
        public string Phone { get; set; }
    }

}
