using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FundevServer.Data;
using FundevServer.Repositories;
using Microsoft.VisualBasic;
using FundevServer.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FundevServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repo;

        public ProductsController(IProductRepository productRepository)
        {
            _repo = productRepository;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProducts()
        {
            try
            {
                return Ok(await _repo.GetAllProductAsync());
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> GetProduct(int id)
        {
            try
            {
                return Ok( await _repo.GetProductAsync(id));
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ProductModel>>> SearchProducts(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name is can not null");
            }
            try
            {
                var products = await _repo.SearchProductsAsync(name);
                return Ok(products);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutProduct( int id, [FromForm] AddProductModel product)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            if (id == product.Id && product.userId == userId)
            {
                try
                {
                    await _repo.UpdateProductAsync(id, product);
                    return Ok();
                } catch
                {
                    return BadRequest("Cannot update product");
                }
            }
            return BadRequest("The id other product.Id");

        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Product>> PostProduct([FromForm] AddProductModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var uid = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            if (uid == model.userId)
            {
                try
                {
                    var result = await _repo.AddProductAsync(model);
                    return CreatedAtAction("GetProduct", new { id = result.Id }, result);
                }
                catch
                {
                    return BadRequest("Cant add product");
                }
            }
            return BadRequest("Not owner");
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            try
            {
                await _repo.DeleteProductAsync(id, userId);
                return Ok();
            } catch
            {
                return BadRequest();
            }
        }


    }
}
