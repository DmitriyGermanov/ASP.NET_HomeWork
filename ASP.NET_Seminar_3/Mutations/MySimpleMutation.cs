using ASP.NET_Seminar_3.Abstractions;
using ASP.NET_Seminar_3.Services;
using ASP.NET_Seminar3.Models;

namespace ASP.NET_Seminar_3.Mutations
{
    public class MySimpleMutation
    {
        public int AddProduct([Service] IProductService service, ProductDto product) => service.AddProduct(product);
    }
}
