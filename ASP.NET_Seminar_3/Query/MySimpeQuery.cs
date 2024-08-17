using ASP.NET_Seminar_3.Abstractions;
using ASP.NET_Seminar3.Models;

namespace ASP.NET_Seminar_3.Query
{
    public class MySimpeQuery
    {
        public IEnumerable<ProductDto> GetProducts([Service] IProductService productService) => productService.GetProducts();
        public IEnumerable<StorageDto> GetStorages([Service] IStorageService storageService) => storageService.GetStorages();
        public IEnumerable<CategoryDto> GetCategories([Service] ICategoryService categoryService) => categoryService.GetAllCategories();
    }
}
