using e_commerce_store.Data;
using e_commerce_store.Models.DBModels;
using Microsoft.EntityFrameworkCore;

public class OrderProductRepository : IOrderProductRepository
{
    private readonly EcommerceStoreDbContext _dbContext;

    public OrderProductRepository(EcommerceStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<OrderProduct>> GetAllAsync()
    {
        try
        {
            return _dbContext.OrderProduct.ToList();
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

 

    public async Task<OrderProduct> GetByIdAsync(int id)
    {
        return await _dbContext.OrderProduct.Include(op => op.Order)
            .Include(op => op.Product).FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<OrderProduct> CreateAsync(OrderProduct order)
    {
        await _dbContext.OrderProduct.AddAsync(order);
        await _dbContext.SaveChangesAsync();
        return order;
    }

    public async Task<OrderProduct> UpdateAsync(OrderProduct order)
    {
        _dbContext.OrderProduct.Update(order);
        await _dbContext.SaveChangesAsync();
        return order;
    }

    public async Task DeleteAsync(int id)
    {
        var order = await GetByIdAsync(id);
        _dbContext.OrderProduct.Remove(order);
        await _dbContext.SaveChangesAsync();
    }
}


