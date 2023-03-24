
using e_commerce_store.Models.Dto.Order;
using System.ComponentModel.DataAnnotations;

public class UpdateOrderProductDto
    {
    [Key]
    public int Id { get; set; }
    [Required]
    public int Order_ID { get; set; }

    [Required]
    public int Product_ID { get; set; }

    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    [Required]
    public string MembershipName { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }



}

