using ASP.NET_Seminar_3.Abstractions;
using ASP.NET_Seminar3.Models;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace ASP.NET_Seminar_3.Services
{
    public class CategoryService(Seminar3Context dbContext, IMapper mapper, IMemoryCache cache) : ICategoryService
    {
        private readonly Seminar3Context _context = dbContext;
        private readonly IMapper _mapper = mapper;
        private readonly IMemoryCache _cache = cache;
        public int AddCategory(CategoryDto category)
        {
            var entityCategory = _context.Categories.FirstOrDefault(cat => cat.Name != null
                           && cat.Name.Equals(category.Name, StringComparison.OrdinalIgnoreCase));
            if (entityCategory == null)
            {
                entityCategory = _mapper?.Map<Category>(category) ?? throw new Exception("Adding category can't be Null.");

                _context.Categories.Add(entityCategory);
                _context.SaveChanges();
            }

            _cache.Remove("categories");

            return entityCategory.Id;
        }

        public IEnumerable<CategoryDto> GetAllCategories()
        {
            if (_cache.TryGetValue("categories", out List<CategoryDto>? categories) && categories != null)
            {
                return categories;
            }

            categories = [.. _context.Categories.Select(category => _mapper.Map<CategoryDto>(category))];
            _cache.Set("categories", categories, TimeSpan.FromMinutes(30));

            return categories;
        }
    }
}
