using e_commerce_store.Models.DBModels;

public interface IOrderProductRepository
{
    Task<IEnumerable<OrderProduct>> GetAllAsync();
    Task<OrderProduct> GetByIdAsync(int id);
    Task<OrderProduct> CreateAsync(OrderProduct order);
    Task<OrderProduct> UpdateAsync(OrderProduct order);
    Task DeleteAsync(int id);
}
