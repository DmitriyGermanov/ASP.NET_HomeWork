using ASP.NET_HomeWork.Models.DTOs;

namespace ASP.NET_HomeWork.Abstractions
{
    public interface IProductRepository
    {
        public int AddCategory(CategoryDto category);
        public IEnumerable<CategoryDto> GetAllCategories();
        public int AddProduct(ProductDto product);
        public IEnumerable<ProductDto> GetProducts();
    }
}
