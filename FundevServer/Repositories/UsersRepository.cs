using FundevServer.Data;
using FundevServer.Models;
using Microsoft.EntityFrameworkCore;

namespace FundevServer.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly MyDbContext _context;

        public UsersRepository(MyDbContext context) {
            _context = context;
        }
        public async Task<UserModel> GetProfileUser(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if(user == null)
            {
                throw new Exception("User is Null!!!");
            }
            UserModel model;
            if (user.Products != null)
            {
                model = new UserModel()
                {
                    FullName = user.FullName,
                    ProductModels = user.Products != null ? user.Products.Select(x => new ProductModel()
                    {
                        Id = x.Id,
                        ProductName = x.Name,
                        Description = x.Description,
                        Price = x.Price,
                        Quantity = x.Quantity,
                        cateId = x.cateId,
                        userId = x.userId,
                    }).ToList() : null!
                };
                return model;
            }
            model = new UserModel()
            {
                FullName = user.FullName,
                ProductModels = null!
            };
            return model;
        }
    }
}
