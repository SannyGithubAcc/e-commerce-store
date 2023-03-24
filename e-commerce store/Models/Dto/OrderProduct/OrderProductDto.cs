using e_commerce_store.Models.Dto.Order;

public class OrderProductDto
    {
        public int Id { get; set; }
        public int Order_ID { get; set; }
        public int Product_ID { get; set; }
        public decimal Price { get; set; }
        public string MembershipName { get; set; }
        public int Quantity { get; set; }


   }
