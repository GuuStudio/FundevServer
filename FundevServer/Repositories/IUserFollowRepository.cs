

using FundevServer.Models;

namespace FundevServer.Repositories
{
    public interface IUserFollowRepository
    {
        public Task<IEnumerable<FollowModel>> GetFollowsAsync(string storeId );
        public Task<bool> HandleFollowAsync(string followerId , UpdateUserFollowModel model );
        public Task<bool> CheckFollowAsync(string followerId , UpdateUserFollowModel model );

    }
}
