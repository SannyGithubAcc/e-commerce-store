
using e_commerce_store.Models.Dto.Order;
using e_commerce_store.Models.Dto.ProductDto;

public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<ProductDto> CreateProductAsync(CreateUpdateProductDto productDto);
        Task<ProductDto> UpdateProductAsync(int id, CreateUpdateProductDto productDto);
        Task DeleteProductAsync(int id);
    }

