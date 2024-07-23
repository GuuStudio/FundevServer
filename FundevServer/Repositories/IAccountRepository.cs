using FundevServer.Models;
using Microsoft.AspNetCore.Identity;

namespace FundevServer.Repositories
{
    public interface IAccountRepository
    {
        public Task<IdentityResult> SignUpAsync(SignUpModel model);
        public Task<string> SignInAsync(SignInModel model);
    }
}
