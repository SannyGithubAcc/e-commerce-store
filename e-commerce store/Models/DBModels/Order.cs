
using e_commerce_store.Models;
using e_commerce_store.Models.DBModels;

public class Order
{
    public int Id { get; set; }
    public int Customer_ID { get; set; }
    public DateTime Date { get; set; }
    public decimal Price { get; set; }

    public List<OrderProduct> OrderProducts { get; set; }
    public Customer Customer { get; set; }
}