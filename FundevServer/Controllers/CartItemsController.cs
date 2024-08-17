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
    public class CartItemsController : ControllerBase
    {
        private readonly ICartItemRepository _ciRepo;

        public CartItemsController(ICartItemRepository ciRepo) {
            _ciRepo = ciRepo;
        }
        // GET: api/<CartItemsController>
        [HttpGet()]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CartItemModel>>> GetAll( )
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            if (userId ==null ) {
                return Unauthorized("Cant authorize");
            }
            try
            {
                return Ok( await _ciRepo.GetAllCartItemOfCustomAsync(userId));
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddCartItem([FromForm] AddCartItemModel model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            if (userId != model.CustomerId)
            {
                return Unauthorized("Cant authorize");
            }
            try
            {
                await _ciRepo.AddCartItemAsync(model);
                return Ok("Succes Add Cart Item");
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            var customerId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value; 
            if (customerId == null)
            {
               return Unauthorized("Not found user");
            }
            try
            {

               await _ciRepo.RemvoveCartItemAsync(customerId , id);
                return Ok();
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("upQuantity/{id}")]
        [Authorize]
        public async Task<ActionResult<int>> UpQuantity (int id )
        {
            string customerId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            try
            {
                return Ok(await _ciRepo.IncreaseCartItemQuantityAsync(customerId, id));
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("downQuantity/{id}")]
        [Authorize]
        public async Task<ActionResult<int>> DownQuantity (int id )
        {
            string customerId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            try
            {
                return Ok(await _ciRepo.DecreaseCartItemQuantityAsync(customerId, id));
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
