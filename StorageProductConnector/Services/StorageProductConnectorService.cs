using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using StorageProductConnector.Abstractions;
using StorageProductConnector.Context;

namespace StorageProductConnector.Services
{
    public class StorageProductConnectorService(StorageProductConnectorContext context, IMemoryCache cache) : IStorageProductConnectorService
    {
        private readonly StorageProductConnectorContext _context = context;
        private readonly IMemoryCache _cache = cache;

        public async Task<int> AddProductOnStorage(int productId, int storageId)
        {
            if (!await CheckProduct(productId))
                throw new ArgumentException("Product not found.", nameof(productId));
            if (!await CheckStorage(storageId))
                throw new ArgumentException("Storage not found.", nameof(storageId));

            try
            {
                var entity = new ProductStorage
                {
                    ProductId = productId,
                    StorageID = storageId
                };

                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();

                _cache.Remove(productId);
                _cache.Remove(storageId);

                return entity.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> CheckProduct(int productID)
        {
            using var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"https://localhost:7132/api/Product/CheckProduct/{productID}");

            if (response.IsSuccessStatusCode)
            {
                var answer = await response.Content.ReadAsStringAsync();
                if (answer.Equals("true"))
                    return true;
            }

            return false;
        }

        public async Task<bool> CheckStorage(int storageID)
        {
            using var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"https://localhost:7132/api/Storage/CheckStorage/{storageID}");

            if (response.IsSuccessStatusCode)
            {
                var answer = await response.Content.ReadAsStringAsync();
                if (answer.Equals("true"))
                    return true;
            }

            return false;
        }

        public IEnumerable<int> GetProductsId(int storageId)
        {
            if (_cache.TryGetValue(storageId, out List<int>? products) && products != null)
            {
                return products;
            }
            else
            {
                try
                {
                    var productsId = _context.Set<ProductStorage>()
                                             .Where(ps => ps.StorageID == storageId)
                                             .Select(ps => ps.ProductId)
                                             .ToList();
                    _cache.Set(storageId, productsId);
                    return productsId;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public IEnumerable<int> GetStoragesID(int productId)
        {
            if (_cache.TryGetValue(productId, out List<int>? storages) && storages != null)
            {
                return storages;
            }
            else
            {
                try
                {
                    var storagesId = _context.Set<ProductStorage>()
                                             .Where(ps => ps.ProductId == productId)
                                             .Select(ps => ps.StorageID)
                                             .ToList();
                    _cache.Set(productId, storagesId);
                    return storagesId;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<int> RemoveProductFromStorage(int productId, int storageId)
        {
            var productStorage = await _context.Set<ProductStorage>()
                                               .FirstOrDefaultAsync(ps => ps.ProductId == productId && ps.StorageID == storageId)
                                               ?? throw new InvalidOperationException("Searching record does't exist.");

            _context.Set<ProductStorage>().Remove(productStorage);
            await _context.SaveChangesAsync();

            _cache.Remove(productId);
            _cache.Remove(storageId);

            return productStorage.Id;
        }
    }
}
