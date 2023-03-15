using System.ComponentModel.DataAnnotations;

namespace e_commerce_store.Dto
{
    public class CustomerUpdateDto
    {
        [Required]
        public string Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public bool? IsActive { get; set; }

        public string? Phone { get; set; }
    }

}
