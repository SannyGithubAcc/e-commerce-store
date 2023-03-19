using AutoMapper;
using e_commerce_store.Models.DBModels;
using e_commerce_store.Models.Dto.ProductDto;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderDto>> GetProductsAsync()
    {
        var products = await _productRepository.GetProductsAsync();
        return _mapper.Map<IEnumerable<OrderDto>>(products);
    }

    public async Task<OrderDto> GetProductByIdAsync(int id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        return _mapper.Map<OrderDto>(product);
    }

    public async Task<OrderDto> CreateProductAsync(CreateUpdateProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        var createdProduct = await _productRepository.CreateProductAsync(product);
        return _mapper.Map<OrderDto>(createdProduct);
    }

    public async Task<OrderDto> UpdateProductAsync(int id, CreateUpdateProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        var updatedProduct = await _productRepository.UpdateProductAsync(id, product);
        return _mapper.Map<OrderDto>(updatedProduct);
    }

    public async Task DeleteProductAsync(int id)
    {
        await _productRepository.DeleteProductAsync(id);
    }
}


