
using e_commerce_store.Models.Dto.ProductDto;

public interface IProductService
    {
        Task<IEnumerable<OrderDto>> GetProductsAsync();
        Task<OrderDto> GetProductByIdAsync(int id);
        Task<OrderDto> CreateProductAsync(CreateUpdateProductDto productDto);
        Task<OrderDto> UpdateProductAsync(int id, CreateUpdateProductDto productDto);
        Task DeleteProductAsync(int id);
    }

