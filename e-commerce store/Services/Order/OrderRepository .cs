using e_commerce_store.Data;
using e_commerce_store.Models;
using e_commerce_store.Models.DBModels;
using Microsoft.EntityFrameworkCore;
using System.Text;

public class OrderRepository : IOrderRepository
{
    private readonly EcommerceStoreDbContext _dbContext;

    public OrderRepository(EcommerceStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        return await _dbContext.Order.FindAsync(id);
    }

    public async Task<List<Order>> GetAllAsync()
    {
        try
        {
            return _dbContext.Order.ToList();
        }

        catch (NotSupportedException ex)
        {
            // catch the exception and print out the error message
            Console.WriteLine("An error occurred: " + ex.Message);
        }
        catch (Exception ex)
        {
            string message = ex.Message;
        }
        return null;
    }

    public async Task<Order> CreateAsync(Order Order)
    {
        await _dbContext.Order.AddAsync(Order);
        ProcessOrder(Order);
        await _dbContext.SaveChangesAsync();
        return Order;
    }

    public async Task<Order> UpdateAsync(Order Order)
    {
        _dbContext.Order.Update(Order);
        await _dbContext.SaveChangesAsync();
        return Order;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var Order = await GetByIdAsync(id);
        if (Order == null)
        {
            return false;
        }

        _dbContext.Order.Remove(Order);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public void ProcessOrder(Order order)
    {
        // Apply business rule 1: activate membership if applicable
        foreach (var orderProduct in order.OrderProducts)
        {
            if (!string.IsNullOrEmpty(orderProduct.MembershipName))
            {
                var membership = _dbContext.Membership.FirstOrDefault(m => m.Name == orderProduct.MembershipName);
                if (membership != null)
                {
                    var customerMembership = new CustomerMembership
                    {
                        CustomerId = order.Customer_ID,
                        Membership = membership,
                        IsActive = true
                    };
                    _dbContext.CustomerMembership.Add(customerMembership);
                }
            }
        }

        // Apply business rule 2: generate shipping slip if applicable
        var hasPhysicalProduct = order.OrderProducts.Any(op => op.Product != null && op.Product.Category == "Physical");
        if (hasPhysicalProduct)
        {
            var customer = _dbContext.Customer.FirstOrDefault(c => c.Id == order.Customer_ID);
            var orderProducts = order.OrderProducts.Where(op => op.Product != null && op.Product.Category == "Physical").ToList();

            // Create shipping slip document
            var shippingSlip = new StringBuilder();
            shippingSlip.AppendLine("Shipping Slip");
            shippingSlip.AppendLine($"Order ID: {order.Id}");
            shippingSlip.AppendLine($"Customer Name: {customer.Name}");
            shippingSlip.AppendLine("Products:");

            foreach (var orderProduct in orderProducts)
            {
                shippingSlip.AppendLine($"{orderProduct.Product.Name} x {orderProduct.Quantity}");
            }
        }

        // Save changes to database
        _dbContext.SaveChanges();
    }

   



}


