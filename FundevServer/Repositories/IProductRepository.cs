using FundevServer.Data;
using FundevServer.Models;

namespace FundevServer.Repositories
{
    public interface IProductRepository
    {
        public Task<IEnumerable<ProductModel>> GetAllProductAsync();
        public Task<ProductModel> GetProductAsync(int Id);
        public Task<Product> AddProductAsync(ProductModel model);
        public Task UpdateProductAsync(int id, ProductModel model);
        public Task DeleteProductAsync(int id, string userId);
    }
}
