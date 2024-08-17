using ASP.NET_Seminar_3.Abstractions;
using ASP.NET_Seminar3.Models;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace ASP.NET_Seminar_3.Services
{
    public class ProductService(Seminar3Context dbContext, IMapper mapper, IMemoryCache cache) : IProductService
    {
        private readonly Seminar3Context _context = dbContext;
        private readonly IMapper _mapper = mapper;
        private readonly IMemoryCache _cache = cache;

        public int AddProduct(ProductDto product)
        {
            var entity = _mapper.Map<Product>(product);
            _context.Add(entity);
            _context.SaveChanges();
            _cache.Remove("products");
            return entity.Id;
        }

        public IEnumerable<ProductDto> GetProducts()
        {
            if (_cache.TryGetValue("products", out List<ProductDto>? products) && products != null)
                return products;

            products = [.. _context.Products.Select(product => _mapper.Map<ProductDto>(product))];

            _cache.Set("products", products, TimeSpan.FromMinutes(30));

            return products;
        }

    }
}
