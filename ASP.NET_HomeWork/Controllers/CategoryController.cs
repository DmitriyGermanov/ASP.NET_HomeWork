using ASP.NET_HomeWork.DTOs;
using ASP.NET_HomeWork.Entities;
using ASP.NET_HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_HomeWork.Controllers
{
    //TO-do: Обновить код на DTO собрав отражения в папку DTOs, смотреть лекция № 2
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpPost("GetCategories")]
        public IActionResult GetCategories()
        {
            try
            {
                using var ctx = new ProductContext();
                var categories = ctx.Categories?
                    .Select(c => new Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                }).ToList();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("AddCategory")]
        public IActionResult AddCategory([FromQuery] int id, [FromQuery] string name, [FromQuery] string? description)
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
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("ChangeCategory")]
        public IActionResult ChangeCategory([FromQuery] int id, [FromQuery] string name, [FromQuery] string? description)
        {
            try
            {
                using var ctx = new ProductContext();
                var category = ctx.Categories?
                    .FirstOrDefault(c => c.Id == id);

                if (category == null)
                {
                    return StatusCode(404, "Category Not Found.");
                }

                category.Name = name;
                category.Description = description;
                ctx.SaveChanges();

                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("DeleteCategory")]
        public IActionResult DeleteCategory([FromQuery] int id)
        {
            try
            {
                using var ctx = new ProductContext();
                var category = ctx.Categories?
                    .FirstOrDefault(c => c.Id == id);

                if (category == null)
                {
                    return StatusCode(404);
                }

                ctx.Remove(category);
                ctx.SaveChanges();

                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPatch("UpdateCategory/{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] PatchCategoryModel patchObject)
        {
            try
            {
                using var ctx = new ProductContext();
                var category = ctx.Categories?
                    .Include(c => c.Products)
                    .FirstOrDefault(c => c.Id == id);

                if (category == null)
                {
                    return NotFound("Category Not Found.");
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
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

     
    }
}
