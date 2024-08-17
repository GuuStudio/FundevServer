using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FundevServer.Data;
using FundevServer.Models;
using Microsoft.AspNetCore.Authorization;
using FundevServer.Repositories;
using System.Security.Claims;

namespace FundevServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly IUsersRepository _usersRepo;

        public UsersController(MyDbContext context, IUsersRepository usersRepository)
        {
            _context = context;
            _usersRepo = usersRepository;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetAllUserModel()
        {
            try
            {
                var users = await _usersRepo.GetAllUsersAsync();
                return Ok(users);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUserModel(string id)
        {
            try
            {
                UserModel userModel = await _usersRepo.GetUserAsync(id);
                return Ok(userModel);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("UpdateAccount")]
        [Authorize]
        public async Task<IActionResult> UpdateAccountModel([FromForm] UpdateForeignInfoAccountModel model)
        {
             if (!ModelState.IsValid) {
                return BadRequest("Type address home or phone number is not valid");
            }
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            if (userId != model.Id) {
                return Unauthorized("Not the same user");
            }
            try
            {
                await _usersRepo.UpdateUserAsync(model);
                return Ok("Update account success");
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUserModel(string id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            if (userId != id)
            {
                return BadRequest("Not the same user");
            }
            await _usersRepo.DeleteUserAsync(userId);
            return Ok();
        }
        [HttpPut("avatar")]
        [Authorize]
        public async Task<IActionResult> ChangeAvatar([FromForm] AvatarModel model )
        {
           var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest("Not found user");
            }
            try
            {
                var result = await _usersRepo.UpdateAvatarAsync(userId, model.formFile);
                return Ok(result);
            } catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("updateUsersName")]
        [Authorize]
        public async Task<IActionResult> ChangeUserFullName( [FromForm] UpdateUserFullName model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Unauthorized("User Id not found");
            }
            try
            {
                await _usersRepo.UpdateUserFullNameAsync(userId, model.fullName);
                return Ok("Success update user name");
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
