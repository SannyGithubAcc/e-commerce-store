using AutoMapper;
using e_commerce_store.Models.Dto.Order;


public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderDto>> GetAllAsync()
    {
        var products = await _orderRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<OrderDto>>(products);
    }

    public async Task<OrderDto> GetByIdAsync(int id)
    {
        var product = await _orderRepository.GetByIdAsync(id);
        return _mapper.Map<OrderDto>(product);
    }

    public async Task<OrderDto> CreateAsync(CreateOrderDto orderDto)
    {
        var order = _mapper.Map<Order>(orderDto);
        var createdProduct = await _orderRepository.CreateAsync(order);
        return _mapper.Map<OrderDto>(createdProduct);
    }

    public async Task<OrderDto> UpdateAsync(UpdateOrderDto updateOrderDto)
    {
        var order = _mapper.Map<Order>(updateOrderDto);
        var updatedProduct = await _orderRepository.UpdateAsync(order);
        return _mapper.Map<OrderDto>(updatedProduct);
    }

    public async Task DeleteAsync(int id)
    {
        await _orderRepository.DeleteAsync(id);
    }


}


