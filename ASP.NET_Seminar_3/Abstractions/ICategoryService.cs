using ASP.NET_Seminar3.Models;

namespace ASP.NET_Seminar_3.Abstractions
{
    public interface ICategoryService
    {
        public int AddCategory(CategoryDto category);
        public IEnumerable<CategoryDto> GetAllCategories();
    }
}
