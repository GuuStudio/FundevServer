using FundevServer.Data;
using FundevServer.Models;
using Microsoft.EntityFrameworkCore;

namespace FundevServer.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly MyDbContext _context;

        public CartItemRepository(MyDbContext context) {
            _context = context;
        }
        public async Task AddCartItemAsync(AddCartItemModel model)
        {
            var productAny = await _context.Products.AnyAsync(p => p.Id == model.ProductId);
            if (!productAny)
            {
                throw new Exception("Product does not exist");
            }
            CartItem cartItem = new CartItem()
            {
                ProductId = model.ProductId,
                UnitPrice = model.UnitPrice,
                Quantity = model.Quantity,
                CustomerId = model.CustomerId,
                StoreId = model.StoreId,
                DateAdded = DateTime.Now,
            };
            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task<int> DecreaseCartItemQuantityAsync( string customerId, int id)
        {
            var cartItem = await _context.CartItems.FirstOrDefaultAsync(x => x.Id == id);
            if (cartItem == null)
            {
                throw new Exception("Cart item not found");
            }
            if (customerId != cartItem.CustomerId)
            {
                throw new Exception("not same Owner");
            }
            if(cartItem.Quantity == 1)
            {
                return cartItem.Quantity;
            }
            cartItem.Quantity -= 1;
            _context.CartItems.Update(cartItem);
            await _context.SaveChangesAsync();
            return cartItem.Quantity;
        }

        public async Task<IEnumerable<CartItemModel>> GetAllCartItemOfCustomAsync(string customId)
        {
            var cartItems = await _context.CartItems
                .Include(x => x.Product)
                .Where(ci => ci.CustomerId == customId).ToListAsync();
            List<CartItemModel> cartItemModels = new List<CartItemModel>();
            if (cartItems.Any())
            {
                cartItemModels = cartItems.Select(ci => new CartItemModel()
                {
                    Id = ci.Id,
                    ProductId =ci.ProductId,
                    UnitPrice =ci.UnitPrice,
                    Quantity = ci.Quantity,
                    CustomerId =ci.CustomerId,
                    StoreId =ci.StoreId,
                    DateAdded = ci.DateAdded,
                    Product = new ProductModel ()
                    {
                        Id = ci.Id,
                        ProductName = ci.Product.Name,
                        Description = ci.Product.Description,
                        Price = ci.Product.Price,
                        ImageUrl = ci.Product.ImageUrl,
                    }
                }).ToList();

            }
            return cartItemModels;
        }

        public async Task<int> IncreaseCartItemQuantityAsync(string customerId, int id)
        {
            
            var cartItem = await _context.CartItems.FirstOrDefaultAsync(x => x.Id == id);
            if (cartItem == null)
            {
                throw new Exception("Cart item not found");
            }
            if (customerId != cartItem.CustomerId)
            {
                throw new Exception("not same Owner");
            }
            cartItem.Quantity += 1;
            _context.CartItems.Update(cartItem);
            await _context.SaveChangesAsync();
            return cartItem.Quantity;
        }

        public async Task RemvoveCartItemAsync( string customerId, int id)
        {

            var cartItems = _context.CartItems.FirstOrDefault(x => x.Id == id);
            if (cartItems == null)
            {
                throw new Exception("Cart Item Is Does Not Exist");
            }
            if (customerId != cartItems.CustomerId)
            {
                throw new Exception("Not same owner");
            }
            _context.CartItems.Remove(cartItems);
            await _context.SaveChangesAsync();
        }
    }
}
