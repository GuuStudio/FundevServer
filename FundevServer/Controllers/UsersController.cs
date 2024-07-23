using FundevServer.Data;
using FundevServer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FundevServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _repo;

        public UsersController(IUsersRepository usersRepository) {
            _repo = usersRepository;
        }
        [HttpGet("profile")]
        [Authorize] // Yêu cầu xác thực
        public async Task<IActionResult> GetUserInfo()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return NotFound("Cant find user by id");
            }
            return Ok(await _repo.GetProfileUser(userId));
        }
    }
}
