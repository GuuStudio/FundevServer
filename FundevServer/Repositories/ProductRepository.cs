 using FundevServer.Data;
using FundevServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FundevServer.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyDbContext _context;
        private readonly ICloudinaryService _cloudinaryService;

        public ProductRepository(MyDbContext context , ICloudinaryService cloudinaryService)  {
            _context = context;
            _cloudinaryService = cloudinaryService;
        }
        public async Task<Product> AddProductAsync(AddProductModel model)
        {

            string imgUrl = await _cloudinaryService.UploadImageAsync(model.FileImage);
            Product newProduct = new()
            {
                  Name = model.ProductName,
                  Description = model.Description,
                  Price = model.Price,
                  Quantity = model.Quantity,
                  Sold = 0,
                  ImageUrl = imgUrl,
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
                var publicId = deleteProduct.ImageUrl.Split('/').Last().Split('.').First();
                var isDeleted = await _cloudinaryService.DeleteImageAsync(publicId);

                if (isDeleted)
                {
                    _context.Products.Remove(deleteProduct);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task<IEnumerable<ProductModel>> GetAllProductAsync()
        {
            var products = await _context.Products
                .Include(p => p.User)
                .Select(p => new ProductModel()
            {
                Id = p.Id,
                ProductName=p.Name,
                Description=p.Description,
                Price = p.Price,
                Quantity = p.Quantity,
                Sold = p.Sold,
                ImageUrl = p.ImageUrl,
                cateId = p.cateId,
                userId = p.userId,
                User = new UserModel()
                   {
                       FullName = p.User.FullName,
                       UserImageUrl = p.User.UserImageUrl,
                       PhoneNumber = p.User.PhoneNumber,
                       AddressHome = p.User.AddressHome,
                   }
                }).ToListAsync();
            return products;
        }

        public async Task<ProductModel> GetProductAsync(int Id)
        {
            var product = await _context.Products
                .Include(p => p.User)
                .FirstOrDefaultAsync( p => p.Id == Id);
            if (product != null)
            {
                ProductModel model = new ProductModel()
                {
                    Id = product.Id,
                    ProductName = product!.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    Sold = product.Sold,
                    ImageUrl = product.ImageUrl,
                    cateId = product.cateId,
                    userId = product.userId,
                    User = new UserModel ()
                    {
                       FullName = product.User.FullName,
                       UserImageUrl = product.User.UserImageUrl,
                       PhoneNumber = product.User.PhoneNumber,
                       AddressHome = product.User.AddressHome,
                    }
                };
                return model;
            }
            return null!;
        }

        public async Task UpdateProductAsync(int id, AddProductModel model)
        {
            Product? product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                // Xử lý cập nhật ảnh nếu có
                if (model.FileImage != null)
                {
                    // Xóa ảnh cũ
                    if (!string.IsNullOrEmpty(product.ImageUrl))
                    {
                        var oldPublicId =  _cloudinaryService.GetPublicIdFromUrl(product.ImageUrl);
                        await _cloudinaryService.DeleteImageAsync(oldPublicId);
                    }

                    // Upload ảnh mới
                    var newImageUrl = await _cloudinaryService.UploadImageAsync(model.FileImage);
                    product.ImageUrl = newImageUrl;
                }
                product.Name = model.ProductName;
                product.Description = model.Description;
                product.Price = model.Price;
                product.Quantity = model.Quantity;
                product.userId = model.userId;
                product.cateId = model.cateId;
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
