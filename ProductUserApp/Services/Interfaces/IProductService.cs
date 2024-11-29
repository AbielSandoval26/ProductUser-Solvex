using ProductUserApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductUserApp.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int productId);
        Task CreateProductAsync(Product product);
        Task UpdateProductAsync(int productId, Product product);
        Task DeleteProductAsync(int productId);
    }
}
