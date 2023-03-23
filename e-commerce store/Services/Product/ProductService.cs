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

    public async Task<IEnumerable<ProductDto>> GetProductsAsync()
    {
        var products = await _productRepository.GetProductsAsync();
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto> GetProductByIdAsync(int id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        return _mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto> CreateProductAsync(CreateUpdateProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        var createdProduct = await _productRepository.CreateProductAsync(product);
        return _mapper.Map<ProductDto>(createdProduct);
    }

    public async Task<ProductDto> UpdateProductAsync(int id, CreateUpdateProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        var updatedProduct = await _productRepository.UpdateProductAsync(id, product);
        return _mapper.Map<ProductDto>(updatedProduct);
    }

    public async Task DeleteProductAsync(int id)
    {
        await _productRepository.DeleteProductAsync(id);
    }
}


