
using e_commerce_store.Models.Dto.Order;

public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task<OrderDto> UpdateAsync(UpdateOrderDto order);
        Task<OrderDto> CreateAsync(CreateOrderDto order);
        Task DeleteAsync(int id);
    Task<OrderDto> GetByIdAsync(int id);
    }

