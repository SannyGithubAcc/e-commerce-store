
using e_commerce_store.Models.Dto.Order;


public interface IOrderProductService
{
        Task<IEnumerable<OrderProductDto>> GetAllAsync();
        Task<OrderProductDto> UpdateAsync(UpdateOrderProductDto order);
        Task<OrderProductDto> CreateAsync(CreateOrderProductDto order);
        Task DeleteAsync(int id);
         Task<OrderProductDto> GetByIdAsync(int id);
    }

