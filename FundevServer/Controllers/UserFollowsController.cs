using FundevServer.Models;
using FundevServer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FundevServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFollowsController : ControllerBase
    {
        private readonly IUserFollowRepository _followRepo;

        public UserFollowsController(IUserFollowRepository userFollowRepo) {
            _followRepo = userFollowRepo;
        }

        [HttpGet]
        [Authorize] 
        public async Task<ActionResult<IEnumerable<FollowModel>>> GetFollows ()
        {
            string storeId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            try
            {
                var result = await _followRepo.GetFollowsAsync(storeId);
                return Ok(result);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("check")]
        [Authorize]
        public async Task<ActionResult<bool>> CheckFollow([FromForm] UpdateUserFollowModel model)
        {
            var followerId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            if (followerId == null)
            {
                return Unauthorized("Follower is not exist");
            }
            try
            {
               bool result =  await _followRepo.CheckFollowAsync(followerId, model);
                return Ok(result);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<UserFollowsController>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<bool>> HandleFollow([FromForm] UpdateUserFollowModel model)
        {
            var followerId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            if (followerId == null)
            {
                return Unauthorized("Follower is not exist");
            }
            try {
                bool result = await _followRepo.HandleFollowAsync(followerId, model);
                return Ok(result);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
