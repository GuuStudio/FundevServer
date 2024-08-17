using FundevServer.Data;
using FundevServer.Models;
using FundevServer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FundevServer.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepo;

        public OrderController(IOrderRepository orderRepo) {
            _orderRepo = orderRepo;
        }
        // GET: OrdersController
        [HttpGet("orders")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<OrderModel>>> GetAllOrdersAsync()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                if (userId == null)
                {
                    return Unauthorized("User is does not exist");
                }
                IEnumerable<OrderModel>? orders = await _orderRepo.GetAllOrdersAsync(userId);
                return Ok(orders);

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }        
        [HttpGet("ordering")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<OrderModel>>> GetAllOrderingAsync()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                if (userId == null)
                {
                    return Unauthorized("User is does not exist");
                }
                IEnumerable<OrderModel>? orders = await _orderRepo.GetAllOrderingAsync(userId);
                return Ok(orders);

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteOrderAsync (int id)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                await _orderRepo.CancelOrderAsync(userId, id);
                return Ok("Success delete order");
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateOrderAsync ( int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            if (userId == null)
            {
                return NotFound("Not found userId");
            }
            try
            {
                await _orderRepo.UpdateOrderAsync(userId, id);
                return Ok("Success Update state order");
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost()]
        [Authorize]
        public async Task<IActionResult> UploadOrderAsync([FromForm] UploadOrderModel model)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            if (userId == null)
            {
                return NotFound("Not found userId");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Form is not valid");
            }
            if (userId == model.StoreId)
            {
                return BadRequest("Unable to order products from your store");
            }
            if (userId == model.CustomerId || userId == model.StoreId)
            {
                try
                {
                    await _orderRepo.UploadOrderAsync(model);
                    return Ok("Success add order");
                } catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest("Product deletion failed");
        }
    }
}
