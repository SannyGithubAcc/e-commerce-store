namespace e_commerce_store.Models.Dto.OrderProduct
{
    public class CreateOrderProductDto
    {
      
        public int Order_ID { get; set; }
        public int Product_ID { get; set; }
        public decimal price { get; set; }
        public string? MembershipName { get; set; }
        public int Quantity { get; set; }
    }
}
