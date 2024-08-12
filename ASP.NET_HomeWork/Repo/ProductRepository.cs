using ASP.NET_HomeWork.Abstractions;
using ASP.NET_HomeWork.Entities;
using ASP.NET_HomeWork.Models;
using ASP.NET_HomeWork.Models.DTOs;
using AutoMapper;

namespace ASP.NET_HomeWork.Repo
{
    public class ProductRepository(IMapper mapper) : IProductRepository
    {
        private readonly IMapper _mapper = mapper;

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

            return entityCategory.Id;
        }

        public IEnumerable<CategoryDto> GetAllCategories()
        {
            using var ctx = new ProductContext();

            return [.. ctx.Categories.Select(category => _mapper.Map<CategoryDto>(category))];
        }

        public IEnumerable<ProductDto> GetProducts()
        {
            using var ctx = new ProductContext();

            return [.. ctx.Products.Select(product => _mapper.Map<ProductDto>(product))];
        }
    }
}
