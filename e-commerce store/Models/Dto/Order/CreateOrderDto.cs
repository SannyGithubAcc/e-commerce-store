﻿namespace e_commerce_store.Models.Dto.Order
{
    public class CreateOrderDto
    {
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
    }
}
