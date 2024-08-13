using ASP.NET_HomeWork.Abstractions;
using ASP.NET_HomeWork.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ASP.NET_HomeWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductRepository productRepository) : ControllerBase
    {
        private readonly IProductRepository _productRepository = productRepository;

        [HttpGet("get_products")]
        public IActionResult GetProducts()
        {
            try
            {
                var products = _productRepository.GetProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("get_products_csv")]
        public FileContentResult GetProductsCsv()
        {
            try
            {
                var products = _productRepository.GetProducts();
                var result = string.Join(Environment.NewLine + Environment.NewLine, products.Select(p => p.ToString()));
                return File(new System.Text.UTF8Encoding().GetBytes(result), "text/csv", "report.csv");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("add_product")]
        public IActionResult AddProduct([FromBody] ProductDto productDto)
        {
            try
            {
                var result = _productRepository.AddProduct(productDto);
                return Ok(result);
            }
            catch (Exception ex)
            { return StatusCode(500, ex); }
        }

        /*        [HttpPut("ChangeProduct")]
                public IActionResult ChangeProduct([FromQuery] int id, [FromQuery] string name, [FromQuery] string? description, [FromQuery] int categoryID)
                {
                    try
                    {
                        using var ctx = new ProductContext();

                        var product = ctx.Products?
                                         .FirstOrDefault(p => p.Id == id);
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

                [HttpDelete("DeleteProduct")]
                public IActionResult DeleteProduct([FromQuery] int id)
                {
                    try
                    {
                        using var ctx = new ProductContext();

                        var product = ctx.Products?
                            .FirstOrDefault(p => p.Id == id);
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

                [HttpPatch("UpdateProduct/{id}")]
                public IActionResult UpdateProduct(int id, [FromBody] PatchProductModel patchObject)
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
                            if (ctx.Categories?
                                   .Include(c => c.Products)
                                   .FirstOrDefault(category => category.Id == patchObject.CategoryID) != null)
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

                [HttpPatch("SetProductPrice/{id}")]
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
                }*/
    }
}
