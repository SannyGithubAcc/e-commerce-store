
public interface IOrderRepository
{

            Task<Order> GetByIdAsync(int id);
            Task<List<Order>> GetAllAsync();
            Task<Order> CreateAsync(Order membership);
            Task<Order> UpdateAsync(Order membership);
            Task<bool> DeleteAsync(int id);
    }

