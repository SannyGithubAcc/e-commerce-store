using System.ComponentModel.DataAnnotations;

namespace e_commerce_store.Dto
{
    public class CustomerUpdateDto
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Phone]
        [StringLength(50)]
        public string Phone { get; set; }
    }

}
