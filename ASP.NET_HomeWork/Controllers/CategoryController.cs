using ASP.NET_HomeWork.Abstractions;
using ASP.NET_HomeWork.Entities;
using ASP.NET_HomeWork.Models;
using ASP.NET_HomeWork.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_HomeWork.Controllers
{
    //TO-do: Обновить код на DTO собрав отражения в папку DTOs, смотреть лекция № 2
    //Строку контекста в Json, управление контекстом через Autofac
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(IProductRepository productRepository) : ControllerBase
    {
        private readonly IProductRepository _productRepository = productRepository;

        [HttpGet("get_categories")]
        public IActionResult GetCategories()
        {
            try
            {
                var categories = _productRepository.GetAllCategories();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("add_category")]
        public IActionResult AddCategory([FromBody] CategoryDto categoryDto)
        {
            try
            {
                var result = _productRepository.AddCategory(categoryDto);
                return Ok(result);
            }
            catch (Exception ex)
            { return StatusCode(500, ex); }
        }

        /*     [HttpPut("ChangeCategory")]
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
             }*/
    }
}
