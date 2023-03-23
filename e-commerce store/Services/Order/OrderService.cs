using AutoMapper;
using e_commerce_store.Models;
using e_commerce_store.Models.Dto.Order;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _OrderRepository;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository OrderRepository, IMapper mapper)
    {
        _OrderRepository = OrderRepository;
        _mapper = mapper;
    }

    public async Task<OrderDto> GetByIdAsync(int id)
    {
        var Order = await _OrderRepository.GetByIdAsync(id);
        return _mapper.Map<OrderDto>(Order);
    }

    public async Task<List<OrderDto>> GetAllAsync()
    {
        var Orders = await _OrderRepository.GetAllAsync();
        return _mapper.Map<List<OrderDto>>(Orders);
    }

    public async Task<OrderDto> CreateAsync(CreateOrderDto createOrderDto)
    {
        var Order = _mapper.Map<Order>(createOrderDto);
        var addedOrder = await _OrderRepository.CreateAsync(Order);

        return _mapper.Map<OrderDto>(addedOrder);
    }

    public async Task<OrderDto> UpdateAsync(int id, UpdateOrderDto updateOrderDto)
    {
        var Order = await _OrderRepository.GetByIdAsync(id);
        if (Order == null)
        {
            return null;
        }

        _mapper.Map(updateOrderDto, Order);
        var updateOrder = await _OrderRepository.UpdateAsync(Order);
        return _mapper.Map<OrderDto>(updateOrder);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _OrderRepository.DeleteAsync(id);
    }


}
