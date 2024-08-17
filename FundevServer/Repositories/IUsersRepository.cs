using FundevServer.Data;
using FundevServer.Models;

namespace FundevServer.Repositories
{
    public interface IUsersRepository
    {
        public Task<IEnumerable<UserModel>> GetAllUsersAsync();
        public Task<UserModel> GetUserAsync(string Id);
        public Task UpdateUserAsync(UpdateForeignInfoAccountModel model);
        public Task DeleteUserAsync(string userId);
        public Task<string> UpdateAvatarAsync(string userId, IFormFile file);
        public Task UpdateUserFullNameAsync(string userId, string fullName);
    }
}
