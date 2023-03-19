namespace e_commerce_store.Models.Dto.Order
{
    public class UpdateOrderDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
    }
}
