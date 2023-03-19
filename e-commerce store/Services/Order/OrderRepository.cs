using e_commerce_store.Data;
using Microsoft.EntityFrameworkCore;

public class OrderRepository : IOrderRepository
{
    private readonly EcommerceStoreDbContext _dbContext;

    public OrderRepository(EcommerceStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        try
        {
            var orders = await _dbContext.Order.ToListAsync();
            return orders;
        }
        catch (Exception ex)
        {
            // Log or handle the exception here
        }
        return null;
    }

 

    public async Task<Order> GetByIdAsync(int id)
    {
        return await _dbContext.Order.FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<Order> CreateAsync(Order order)
    {
        await _dbContext.Order.AddAsync(order);
        await _dbContext.SaveChangesAsync();
        return order;
    }

    public async Task<Order> UpdateAsync(Order order)
    {
        _dbContext.Order.Update(order);
        await _dbContext.SaveChangesAsync();
        return order;
    }

    public async Task DeleteAsync(int id)
    {
        var order = await GetByIdAsync(id);
        _dbContext.Order.Remove(order);
        await _dbContext.SaveChangesAsync();
    }
}


