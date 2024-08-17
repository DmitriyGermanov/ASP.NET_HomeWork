using ASP.NET_Seminar3.Models;

namespace ASP.NET_Seminar_3.Abstractions
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetProducts();
        int AddProduct(ProductDto product);
        bool CheckProduct(int productID);
    }
}
