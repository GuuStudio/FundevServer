using FundevServer.Data;
using FundevServer.Models;

namespace FundevServer.Repositories
{
    public interface IProductRepository
    {
        public Task<IEnumerable<ProductModel>> GetAllProductAsync();
        public Task<IEnumerable<ProductModel>> SearchProductsAsync(string name);
        public Task<ProductModel> GetProductAsync(int Id);
        public Task<Product> AddProductAsync(AddProductModel model);
        public Task UpdateProductAsync(int id, AddProductModel model);
        public Task DeleteProductAsync(int id, string userId);
    }
}
