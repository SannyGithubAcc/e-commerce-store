using e_commerce_store.Models.Dto.Order;

    public interface IOrderService
    {
        Task<OrderDto> GetByIdAsync(int id);
        Task<List<OrderDto>> GetAllAsync();
        Task<OrderDto> CreateAsync(CreateOrderDto createOrderDto);
        Task<OrderDto> UpdateAsync(int id, UpdateOrderDto updateOrderDto);
        Task<bool> DeleteAsync(int id);
        

}


