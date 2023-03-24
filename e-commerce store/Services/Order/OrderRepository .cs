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
        //ProcessOrder(Order);
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

   

   



}


