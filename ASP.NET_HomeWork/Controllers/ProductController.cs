using ASP.NET_HomeWork.Entities;
using ASP.NET_HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_HomeWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("getProducts")]
        public IActionResult GetProducts()
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
                ctx.SaveChanges();

                return Ok(product);

            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("deleteProduct")]
        public IActionResult DeleteProduct([FromQuery] int id)
        {
            try
            {
                using var ctx = new ProductContext();

                var product = ctx.Products?.FirstOrDefault(p => p.Id == id);
                if (product == null)
                {
                    return StatusCode(404);
                }
                ctx.Remove(product);
                ctx.SaveChanges();

                return Ok(product);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPatch("patchProduct/{id}")]
        public IActionResult PatchProduct(int id, [FromBody] PatchProductModel patchObject)
        {
            try
            {
                using var ctx = new ProductContext();

                var product = ctx.Products?
                   .Include(p => p.ProductGroup)
                   .Include(product => product.ProductStorages)
                   .FirstOrDefault(p => p.Id == id);
                if (product == null)
                {
                    return NotFound("Product Not Found");
                }

                if (patchObject.Name != null)
                {
                    product.Name = patchObject.Name;
                }
                if (patchObject.Description != null)
                {
                    product.Description = patchObject.Description;
                }
                if (patchObject.CategoryID.HasValue)
                {
                    if (ctx.Categories?.Include(c => c.Products).FirstOrDefault(category => category.Id == patchObject.CategoryID) != null)
                        product.CategoryID = patchObject?.CategoryID.Value;
                    else
                        return NotFound("Category Not Found");
                }

                ctx.SaveChanges();

                return Ok(product);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPatch("setProductPrice/{id}")]
        public IActionResult SetProductPrice(int id, [FromQuery] int cost)
        {
            try
            {
                using var ctx = new ProductContext();
                var product = ctx.Products?
                    .Include(p => p.ProductGroup)
                    .Include(product => product.ProductStorages)
                    .FirstOrDefault(p => p.Id == id);

                if (product == null)
                {
                    return NotFound();
                }

                product.Cost = cost;
                ctx.SaveChanges();

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public class PatchProductModel
        {
            public string? Name { get; set; }
            public string? Description { get; set; }
            public int? CategoryID { get; set; }
        }
    }
}
