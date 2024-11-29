using ProductUserApp.Models;
using ProductUserApp.Services.Interfaces;
using System.Net.Http.Json;


namespace ProductUserApp.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Product>>("api/Products");
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _httpClient.GetFromJsonAsync<Product>($"api/Products/{productId}");
        }

        public async Task CreateProductAsync(Product product)
        {
            await _httpClient.PostAsJsonAsync("api/Products", product);
        }

        public async Task UpdateProductAsync(int productId, Product product)
        {
            await _httpClient.PutAsJsonAsync($"api/Products/{productId}", product);
        }

        public async Task DeleteProductAsync(int productId)
        {
            await _httpClient.DeleteAsync($"api/Products/{productId}");
        }
    }
}
