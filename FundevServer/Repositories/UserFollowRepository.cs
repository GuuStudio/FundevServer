using FundevServer.Data;
using FundevServer.Models;
using Microsoft.EntityFrameworkCore;

namespace FundevServer.Repositories
{
    public class UserFollowRepository : IUserFollowRepository
    {
        private readonly MyDbContext _context;

        public UserFollowRepository(MyDbContext context) {
            _context = context;
        }

        public async Task<bool> CheckFollowAsync(string followerId, UpdateUserFollowModel model)
        {
            return await _context.UserFollows.AnyAsync(uf => uf.FollowerId == followerId && uf.StoreId == model.StoreId);
        }

        public async Task<IEnumerable<FollowModel>> GetFollowsAsync(string storeId)
        {
            var follows = await _context.UserFollows
                .Include(uf => uf.Store)
                .Include(uf => uf.Follower)
                .Where(uf => uf.StoreId == storeId).Select(uf => new FollowModel
            {
                Id = uf.Id,
                StoreId = uf.StoreId,
                FollowerId = uf.FollowerId,
                CreatedAt = uf.CreatedAt,
                Store = new UserModel
                {
                    FullName = uf.Store.FullName,
                },
                Follower = new UserModel
                {
                    FullName = uf.Store.FullName,
                }

            }).ToListAsync();
           
            return follows;
        }

        public async Task<bool> HandleFollowAsync(string followerId, UpdateUserFollowModel model)
        {
            var follow = _context.UserFollows.SingleOrDefault(uf => uf.FollowerId == followerId && uf.StoreId == model.StoreId);
            if (follow == null) 
            {
                UserFollow userFollow = new UserFollow()
                {
                    StoreId = model.StoreId,
                    FollowerId = followerId,
                    CreatedAt = DateTime.Now,
                };
                _context.UserFollows.Add(userFollow);
                await _context.SaveChangesAsync();
                return true;

            } else
            {
                _context.UserFollows.Remove(follow);
                await _context.SaveChangesAsync();
                return false;
            }

        }
        
    }
}
