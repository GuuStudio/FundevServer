

using FundevServer.Models;

namespace FundevServer.Repositories
{
    public interface IUserFollowRepository
    {
        public Task<bool> HandleFollowAsync(string followerId , UpdateUserFollowModel model );
        public Task<bool> CheckFollowAsync(string followerId , UpdateUserFollowModel model );
    }
}
