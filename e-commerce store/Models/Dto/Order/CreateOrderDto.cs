using e_commerce_store.Dto;
using System.ComponentModel.DataAnnotations;

namespace e_commerce_store.Models.Dto.Order
{
    public class CreateOrderDto
    {


        [Required]
        public int Customer_ID { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

       
      
    }


}
