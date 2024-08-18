using FundevServer.Data;
using FundevServer.Helpers;
using FundevServer.Models;
using Microsoft.EntityFrameworkCore;

namespace FundevServer.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MyDbContext _context;

        public OrderRepository(MyDbContext context)
        {
            _context = context;
        }
        public async Task CancelOrderAsync( string userId,int id)
        {
            Order? order = _context.Orders.SingleOrDefault(o => o.OrderId == id);
            if (order == null) {
                throw new Exception("Order does not exist");
            }
            if (userId != order.CustomerId)
            {
                throw new Exception("Unauthorize");
            }
            if (order.Status == OrderStatus.Pending)
            {
                order.Status = OrderStatus.Cancelled;
                order.CanceledDate = DateTime.Now;
                await _context.SaveChangesAsync();
            } else
            {
                throw new Exception("Unable to cancel order");
            }
        }

        public async Task<IEnumerable<OrderModel>> GetAllOrderingAsync(string id)
        {
            var orders = await _context.Orders
                .Include(o => o.Product)
                .Include(o => o.Customer)
                .Include(o => o.Store)
                .Where(o => o.CustomerId == id).Select( o => new OrderModel ()
                {
                    OrderId = o.OrderId,
                    ProductId = o.ProductId,
                    Quantity = o.Quantity,
                    CustomerId = o.CustomerId,
                    StoreId = o.StoreId,
                    OrderDate = o.OrderDate,
                    Status = o.Status.ToString(),
                    TotalAmount = o.TotalAmount,
                    ShippingAddress = o.ShippingAddress,
                    ShippingDate = o.ShippingDate,
                    CompletionDate = o.CompletionDate,
                    CanceledDate = o.CanceledDate,
                    Product = new ProductModel ()
                    {
                        ProductName = o.Product.Name,
                        Description = o.Product.Description,
                        Price = o.Product.Price,
                        ImageUrl = o.Product.ImageUrl,
                        
                    },
                    Customer = new UserModel
                    {
                        FullName = o.Customer.FullName,
                    },
                    Store = new UserModel {
                        FullName = o.Store.FullName,
                    },

                }).ToListAsync();
            return orders;
        }

        public async Task<IEnumerable<OrderModel>> GetAllOrdersAsync(string id)
        {
            var orders = await _context.Orders
                .Include(o => o.Product)
                .Include(o => o.Customer)
                .Include(o => o.Store)
                .Where(o => o.StoreId == id)
                .Select(o => new OrderModel()
                {
                    OrderId = o.OrderId,
                    ProductId = o.ProductId,
                    Quantity = o.Quantity,
                    CustomerId = o.CustomerId,
                    StoreId = o.StoreId,
                    OrderDate = o.OrderDate,
                    Status = o.Status.ToString(),
                    TotalAmount = o.TotalAmount,
                    ShippingAddress = o.ShippingAddress,
                    ShippingDate = o.ShippingDate,
                    CompletionDate = o.CompletionDate,
                    CanceledDate = o.CanceledDate,
                    Product = new ProductModel()
                    {
                        ProductName = o.Product.Name,
                        Description = o.Product.Description,
                        Price = o.Product.Price,
                        ImageUrl = o.Product.ImageUrl,

                    },
                    Customer = new UserModel
                    {
                        FullName = o.Customer.FullName,
                    },
                    Store = new UserModel
                    {
                        FullName = o.Store.FullName,
                    },

                }).ToListAsync();
            return orders;
        }

        public async Task UpdateOrderAsync(string userId, int id)
        {
            
            Order? order = _context.Orders.SingleOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                throw new Exception("Order does not exist here");
            }
            if (order.Status == OrderStatus.Pending && userId == order.StoreId)
            {
                order.ShippingDate = DateTime.Now;
                order.Status = OrderStatus.Shipped;
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
            }
            if (order.Status == OrderStatus.Shipped && userId == order.CustomerId)
            {
                order.CompletionDate = DateTime.Now;
                order.Status = OrderStatus.Completed;
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
            }
       }

        public async Task UploadOrderAsync(UploadOrderModel model)
        {
            Product? product = _context.Products.SingleOrDefault(p => p.Id ==  model.ProductId);
            if (product == null)
            {
                throw new Exception("Product is does not exist");
            }
            Order order = new Order
            {
                ProductId = model.ProductId,
                Quantity = model.Quantity,
                OrderDate = DateTime.Now,
                PhoneNumber = model.PhoneNumber,
                CustomerId = model.CustomerId,
                StoreId = model.StoreId,
                Status = OrderStatus.Pending,
                TotalAmount = product.Price*model.Quantity,
                ShippingAddress = model.ShippingAddress,

            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }
    }
}
