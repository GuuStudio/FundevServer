using FundevServer.Data;
using FundevServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FundevServer.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly MyDbContext _context;
        private readonly ICloudinaryService _cloudinary;

        public UsersRepository(MyDbContext context , ICloudinaryService cloudinaryService) {
            _context = context;
            _cloudinary = cloudinaryService;
        }

   

        public async Task DeleteUserAsync(string userId)
        {
            var userDelete = await _context.Users.FindAsync(userId);
            if (userDelete != null)
            {
                var Follower = await _context.UserFollows.Where( uf => uf.StoreId == userId).ToListAsync();
                var Following = await _context.UserFollows.Where( uf => uf.FollowerId == userId).ToListAsync();
                if (userDelete.UserImageUrl != null)
                {
                    var imagePublicId = _cloudinary.GetPublicIdFromUrl(userDelete.UserImageUrl);
                    bool isDelete = await _cloudinary.DeleteImageAsync(imagePublicId);
                    if (isDelete)
                    {
                        _context.UserFollows.RemoveRange(Follower);
                        _context.UserFollows.RemoveRange(Following);
                        _context.Users.Remove(userDelete);
                        await _context.SaveChangesAsync();
                    } else
                    {
                        throw new Exception("Cant delete image");
                    }
                } else
                {
                    _context.UserFollows.RemoveRange(Follower);
                    _context.UserFollows.RemoveRange(Following);
                    _context.Users.Remove(userDelete);
                    await _context.SaveChangesAsync();
                }
            } else
            {
                throw new Exception("Cant find user delete");
            }
        }

        public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            var users = await _context.Users.Select( u => new UserModel()
            {
                Id = u.Id,
                FullName = u.FullName,
                CreateAt = u.CreateAt,
                AddressHome = u.AddressHome,
                UserImageUrl = u.UserImageUrl,
            }).ToListAsync();
            return users;
        }

        public async Task<UserModel> GetUserAsync(string Id)
        {
            var user = await _context.Users
                .Include(u => u.Products)
                .Include(u => u.Followers)
                .Include(u => u.Following)
                .FirstOrDefaultAsync(u => u.Id == Id);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            return new UserModel
            {
                Id = user.Id,
                FullName = user.FullName,
                CreateAt = user.CreateAt,
                AddressHome = user.AddressHome ?? string.Empty,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                UserImageUrl = user.UserImageUrl ?? string.Empty,
                ProductModels = user.Products.Select(p => new ProductModel  
                {
                    Id = p.Id,
                    ProductName = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    ImageUrl = p.ImageUrl,
                    cateId = p.cateId,
                    userId = p.userId,
                }).ToList(),
                Followers = user.Followers,
                Following = user.Following,
            };
        }

        public async Task<string> UpdateAvatarAsync(string userId, IFormFile file)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) {
                throw new Exception("Not Found User");
            } 
            if (user.UserImageUrl != null)
            {
                var publicImageId = _cloudinary.GetPublicIdFromUrl(user.UserImageUrl);
                if (publicImageId != null)
                {
                   await  _cloudinary.DeleteImageAsync(publicImageId);
                }
            }
            string imageUrl = await _cloudinary.UploadImageAsync(file); 
            user.UserImageUrl = imageUrl;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return imageUrl;
        }

        public async Task UpdateUserAsync( UpdateForeignInfoAccountModel model)
        {
            ApplicationUser? user = await _context.Users.FindAsync(model.Id);
            if (user == null)
            {
                throw new Exception("Not found user");
            }
            user.AddressHome = model.AddressHome ?? string.Empty;
            user.PhoneNumber = model.PhoneNumber ?? string.Empty;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserFullNameAsync(string userId, string fullName)
        {
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                throw new Exception("User Not Found");
            }
            user.FullName = fullName;
            _context.Users.Update(user);
            try
            {
                await _context.SaveChangesAsync();
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        } 
    }
}
