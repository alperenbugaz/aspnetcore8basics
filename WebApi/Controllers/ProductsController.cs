using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.DTO;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {

        private readonly ProductsContext _context;

        public ProductsController(ProductsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {

            var products = await _context.Products
                .Where(p => p.IsAvailable == true)
                .Select(p => ProductDTO(p))
                .ToListAsync();
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }
        
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(ProductDTO(product));
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int? id, Product product)
        {
            if (id == null || product == null)
            {
                return BadRequest();
            }

            if (id != product.ProductId)
            {
                return BadRequest();
            }

            var p = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            if (p == null)
            {
                return NotFound();
            }

            p.ProductName = product.ProductName;
            p.Price = product.Price;
            p.IsAvailable = product.IsAvailable;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return NotFound();
            }


            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var p = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            if (p == null)
            {
                return NotFound();
            }

            _context.Products.Remove(p);
            await _context.SaveChangesAsync();

            return Ok(p);
        }

        private static ProductDTO ProductDTO(Product p)
        {   
            var entity = new ProductDTO();

            if (p != null)
            {
                entity.ProductId = p.ProductId;
                entity.ProductName = p.ProductName;
                entity.Price = p.Price;
            }

            return entity;

        }

    }
}