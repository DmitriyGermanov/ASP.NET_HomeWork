using ASP.NET_HomeWork.Abstractions;
using ASP.NET_HomeWork.Entities;
using ASP.NET_HomeWork.Models;
using ASP.NET_HomeWork.Models.DTOs;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace ASP.NET_HomeWork.Repo
{
    public class ProductRepository(IMapper mapper, IMemoryCache memoryCache) : IProductRepository
    {
        private readonly IMapper _mapper = mapper;
        private readonly IMemoryCache _cache = memoryCache;

        public int AddCategory(CategoryDto category)
        {
            using var ctx = new ProductContext() ?? throw new Exception("Context can't be null.");

            var entityCategory = ctx.Categories.FirstOrDefault(cat => cat.Name != null
                                 && cat.Name.Equals(category.Name, StringComparison.OrdinalIgnoreCase));
            if (entityCategory == null)
            {
                entityCategory = _mapper?.Map<Models.Category>(category) ?? throw new Exception("Adding category can't be null.");

                ctx.Categories.Add(entityCategory);
                ctx.SaveChanges();
            }

            _cache.Remove("categories");

            return entityCategory.Id;
        }

        public int AddProduct(ProductDto product)
        {

            using var ctx = new ProductContext() ?? throw new Exception("Context can't be null.");

            var entityCategory = ctx.Categories.FirstOrDefault(cat => cat.Name != null
                                 && cat.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase));
            if (entityCategory == null)
            {
                entityCategory = _mapper?.Map<Models.Category>(product) ?? throw new Exception("Adding product can't be null.");

                ctx.Categories.Add(entityCategory);
                ctx.SaveChanges();
            }

            _cache.Remove("products");

            return entityCategory.Id;
        }

        public IEnumerable<CategoryDto> GetAllCategories()
        {
            if (_cache.TryGetValue("categories", out List<CategoryDto>? categories) && categories != null)
            {
                return categories;
            }
            using var ctx = new ProductContext();

            
            categories = ctx.Categories.Select(category => _mapper.Map<CategoryDto>(category)).ToList();
            _cache.Set("categories", categories, TimeSpan.FromMinutes(30));
            
            return categories;
        }

        public IEnumerable<ProductDto> GetProducts()
        {
            if (_cache.TryGetValue("products", out List<ProductDto>? products) && products != null)
            {
                return products;
            }

            using var ctx = new ProductContext();

            products = ctx.Products.Select(product => _mapper.Map<ProductDto>(product)).ToList();

            _cache.Set("products", products, TimeSpan.FromMinutes(30));
            
            return products;
        }
    }
}
