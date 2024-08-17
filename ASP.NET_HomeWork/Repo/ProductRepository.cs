using ASP.NET_HomeWork.Abstractions;
using ASP.NET_HomeWork.Entities;
using ASP.NET_HomeWork.Models.DTOs;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace ASP.NET_HomeWork.Repo
{
    public class ProductRepository(IMapper mapper, IMemoryCache memoryCache, ProductContext productContext) : IProductRepository
    {
        private readonly IMapper _mapper = mapper;
        private readonly IMemoryCache _cache = memoryCache;
        private readonly ProductContext _productContext = productContext;

        public int AddCategory(CategoryDto category)
        {
            var entityCategory = _productContext.Categories.FirstOrDefault(cat => cat.Name != null
                                 && cat.Name.Equals(category.Name, StringComparison.OrdinalIgnoreCase));
            if (entityCategory == null)
            {
                entityCategory = _mapper?.Map<Models.Category>(category) ?? throw new Exception("Adding category can't be null.");

                _productContext.Categories.Add(entityCategory);
                _productContext.SaveChanges();
            }

            _cache.Remove("categories");

            return entityCategory.Id;
        }

        public int AddProduct(ProductDto product)
        {

            var entityProduct = _productContext.Products.FirstOrDefault(cat => cat.Name != null
                                 && cat.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase));
            if (entityProduct == null)
            {
                entityProduct = _mapper?.Map<Models.Product>(product) ?? throw new Exception("Adding product can't be null.");

                _productContext.Products.Add(entityProduct);
                _productContext.SaveChanges();
            }

            _cache.Remove("products");

            return entityProduct.Id;
        }

        public IEnumerable<CategoryDto> GetAllCategories()
        {
            if (_cache.TryGetValue("categories", out List<CategoryDto>? categories) && categories != null)
            {
                return categories;
            }

            categories = [.. _productContext.Categories.Select(category => _mapper.Map<CategoryDto>(category))];
            _cache.Set("categories", categories, TimeSpan.FromMinutes(30));

            return categories;
        }

        public IEnumerable<ProductDto> GetProducts()
        {
            if (_cache.TryGetValue("products", out List<ProductDto>? products) && products != null)
            {
                return products;
            }

            products = [.. _productContext.Products.Select(product => _mapper.Map<ProductDto>(product))];

            _cache.Set("products", products, TimeSpan.FromMinutes(30));

            return products;
        }
    }
}
