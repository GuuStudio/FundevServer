using FundevServer.Data;
using FundevServer.Models;

namespace FundevServer.Repositories
{
    public interface IUsersRepository
    {
        public Task<UserModel> GetProfileUser(string id);
    }
}
