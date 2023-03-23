namespace e_commerce_store.Models.DBModels
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public int Order_ID { get; set; }
        public int Product_ID { get; set; }
        public decimal Price { get; set; }
        public string MembershipName { get; set; }
        public int Quantity { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}

