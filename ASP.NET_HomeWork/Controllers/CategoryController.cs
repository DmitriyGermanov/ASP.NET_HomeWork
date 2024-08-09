using ASP.NET_HomeWork.Entities;
using ASP.NET_HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_HomeWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpPost("getCategories")]
        public IActionResult GetCategories()
        {
            try
            {
                using var ctx = new ProductContext();
                var categories = ctx.Categories?.Select(c => new Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                }).ToList();
                return Ok(categories);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("postCategory")]
        public IActionResult PostCategory([FromQuery] int id, [FromQuery] string name, [FromQuery] string? description)
        {
            try
            {
                using var ctx = new ProductContext();
                var existingCategory = ctx.Categories?
                    .FirstOrDefault(c => c.Id == id);

                if (existingCategory != null)
                {
                    return Conflict("Category already exists.");
                }

                var newCategory = new Category
                {
                    Id = id,
                    Name = name,
                    Description = description ?? "",
                };

                ctx.Categories?.Add(newCategory);
                ctx.SaveChanges();

                return Ok(newCategory);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut("putCategory")]
        public IActionResult PutCategory([FromQuery] int id, [FromQuery] string name, [FromQuery] string? description)
        {
            try
            {
                using var ctx = new ProductContext();
                var category = ctx.Categories?.FirstOrDefault(c => c.Id == id);

                if (category == null)
                {
                    return StatusCode(404);
                }

                category.Name = name;
                category.Description = description;
                ctx.SaveChanges();

                return Ok(category);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("deleteCategory")]
        public IActionResult DeleteCategory([FromQuery] int id)
        {
            try
            {
                using var ctx = new ProductContext();
                var category = ctx.Categories?.FirstOrDefault(c => c.Id == id);

                if (category == null)
                {
                    return StatusCode(404);
                }

                ctx.Remove(category);
                ctx.SaveChanges();

                return Ok(category);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPatch("patchCategory/{id}")]
        public IActionResult PatchCategory(int id, [FromBody] PatchCategoryModel patchObject)
        {
            try
            {
                using var ctx = new ProductContext();
                var category = ctx.Categories?
                    .Include(c => c.Products)
                    .FirstOrDefault(c => c.Id == id);

                if (category == null)
                {
                    return NotFound("Category Not Found");
                }

                if (patchObject.Name != null)
                {
                    category.Name = patchObject.Name;
                }
                if (patchObject.Description != null)
                {
                    category.Description = patchObject.Description;
                }

                ctx.SaveChanges();

                return Ok(category);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        public class PatchCategoryModel
        {
            public string? Name { get; set; }
            public string? Description { get; set; }
        }
    }
}
