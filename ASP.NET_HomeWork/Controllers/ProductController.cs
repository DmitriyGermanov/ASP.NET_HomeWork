using ASP.NET_HomeWork.Entities;
using ASP.NET_HomeWork.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_HomeWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("getProduct")]
        public IActionResult GetProduct()
        {
            try
            {
                using var ctx = new ProductContext();

                var products = ctx.Products?.Select(p => new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                }).ToList();
                return Ok(products);

            }
            catch
            {
                return StatusCode(500);
            }

        }
        [HttpPost("postProduct")]
        public IActionResult PostProduct([FromQuery] int id, [FromQuery] string name, [FromQuery] string? description, [FromQuery] int categoryID)
        {
            try
            {
                using var ctx = new ProductContext();
                var category = ctx.Categories?.FirstOrDefault(c => c.Id == categoryID);
                var existingProduct = ctx.Products?
                    .FirstOrDefault(p => p.Id == id);

                if (existingProduct != null)
                {
                    return Conflict("Product already exists.");
                }

                var newProduct = new Product
                {
                    Id = id,
                    Name = name,
                    CategoryID = category?.Id,
                    Description = description ?? "",
                };

                ctx.Products?.Add(newProduct);
                ctx.SaveChanges();

                return Ok(newProduct);
            }
            catch 
            {
                return StatusCode(500);
            }
        }

        [HttpPut("putProduct")]
        public IActionResult PutProduct([FromQuery] int id, [FromQuery] string name, [FromQuery] string? description, [FromQuery] int categoryID)
        {
            try
            {
                using var ctx = new ProductContext();

                var product = ctx.Products?.FirstOrDefault(p => p.Id == id);
                if (product == null)
                {
                    return StatusCode(404);
                }
                product.Name = name;
                product.Description = description;
                product.CategoryID = categoryID;
                ctx.Products?.Update(product);
                ctx.SaveChanges();

                return Ok(product);

            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
