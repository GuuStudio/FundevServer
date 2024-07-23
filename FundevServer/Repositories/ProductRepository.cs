using FundevServer.Data;
using FundevServer.Models;
using Microsoft.EntityFrameworkCore;

namespace FundevServer.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyDbContext _context;

        public ProductRepository(MyDbContext context)  {
            _context = context;
        }
        public async Task<Product> AddProductAsync(ProductModel model)
        {
            Product newProduct = new()
            {
                  Name = model.ProductName,
                  Description = model.Description,
                  Price = model.Price,
                  Quantity = model.Quantity,
                  cateId  = model.cateId,
                  userId = model.userId,
            };
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
            return newProduct;
        }

        public async Task DeleteProductAsync(int id, string userId)
        {
            var deleteProduct =  _context.Products.SingleOrDefault(a => a.Id == id);
            
            if (deleteProduct != null && userId == deleteProduct.userId)
            {
                _context.Products.Remove(deleteProduct);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ProductModel>> GetAllProductAsync()
        {
            var products = await _context.Products.Select(a => new ProductModel()
            {
                Id = a.Id,
                ProductName=a.Name,
                Description=a.Description,
                Price = a.Price,
                Quantity = a.Quantity,
                cateId = a.cateId,
                userId = a.userId,
                
            }).ToListAsync();
            return products;
        }

        public async Task<ProductModel> GetProductAsync(int Id)
        {
            var product = await _context.Products.FindAsync(Id);
            if (product != null)
            {
                ProductModel model = new ProductModel()
                {
                    Id = product.Id,
                    ProductName = product!.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    cateId = product.cateId,
                    userId = product.userId,
                };
                return model;
            }
            return null!;
        }

        public async Task UpdateProductAsync(int id, ProductModel model)
        {
            if(id == model.Id)
            {
                var updateProduct = new Product()
                {
                    Name = model.ProductName,
                    Description = model.Description,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    cateId = model.cateId,
                    userId = model.userId,
                };
                _context.Products.Update(updateProduct);
                await _context.SaveChangesAsync();

            }
        }
    }
}
