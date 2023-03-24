using AutoMapper;
using e_commerce_store.Models.DBModels;
using e_commerce_store.Models.Dto.Order;

public class OrderProductService : IOrderProductService
{
    private readonly IOrderProductRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderProductService(IOrderProductRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderProductDto>> GetAllAsync()
    {
        var products = await _orderRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<OrderProductDto>>(products);
    }

    public async Task<OrderProductDto> GetByIdAsync(int id)
    {
        var product = await _orderRepository.GetByIdAsync(id);
        return _mapper.Map<OrderProductDto>(product);
    }

    public async Task<OrderProductDto> CreateAsync(CreateOrderProductDto OrderProductDto)
    {
        var order = _mapper.Map<OrderProduct>(OrderProductDto);
        var createdProduct = await _orderRepository.CreateAsync(order);
        return _mapper.Map<OrderProductDto>(createdProduct);
    }

    public async Task<OrderProductDto> UpdateAsync(UpdateOrderProductDto updateOrderProductDto)
    {
        var order = _mapper.Map<OrderProduct>(updateOrderProductDto);
        var updatedProduct = await _orderRepository.UpdateAsync(order);
        return _mapper.Map<OrderProductDto>(updatedProduct);
    }

    public async Task DeleteAsync(int id)
    {
        await _orderRepository.DeleteAsync(id);
    }


}


