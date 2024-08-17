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

        public async Task<bool> HandleFollowAsync(string followerId, UpdateUserFollowModel model)
        {
            var follow = _context.UserFollows.SingleOrDefault(uf => uf.FollowerId == followerId && uf.StoreId == model.StoreId);
            if (follow != null) 
            {
                _context.Remove(follow);
                await _context.SaveChangesAsync();
                return false;
            }
            UserFollow userFollow = new UserFollow()
            {
                StoreId = model.StoreId,
                FollowerId = followerId,
                CreatedAt = DateTime.Now,
            };
            return true;
        }
    }
}
