using System.ComponentModel.DataAnnotations;

namespace e_commerce_store.Models.Dto.ProductDto
{
    public class CreateUpdateProductDto
    {

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Barcode { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(255)]
        public string Category { get; set; }


    }
}
