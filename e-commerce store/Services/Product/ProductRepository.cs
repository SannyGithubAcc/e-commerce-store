

using e_commerce_store.Data;
using e_commerce_store.Models.DBModels;
using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductRepository
{
    private readonly EcommerceStoreDbContext _dbContext;

    public ProductRepository(EcommerceStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await _dbContext.Product.ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _dbContext.Product.FindAsync(id);
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        await _dbContext.Product.AddAsync(product);
        await _dbContext.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateProductAsync(int id, Product product)
    {
        var existingProduct = await _dbContext.Product.FindAsync(id);
                if (existingProduct == null)
        {
            throw new ArgumentException($"Product with ID {id} not found");
        }

        existingProduct.Name = product.Name;
        existingProduct.Barcode = product.Barcode;
        existingProduct.IsActive = product.IsActive;
        existingProduct.Description = product.Description;
        existingProduct.Price = product.Price;
        existingProduct.Category = product.Category;

        await _dbContext.SaveChangesAsync();

        return existingProduct;
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _dbContext.Product.FindAsync(id);
        if (product == null)
        {
            throw new ArgumentException($"Product with ID {id} not found");
        }

        _dbContext.Product.Remove(product);
        await _dbContext.SaveChangesAsync();
    }

    
}