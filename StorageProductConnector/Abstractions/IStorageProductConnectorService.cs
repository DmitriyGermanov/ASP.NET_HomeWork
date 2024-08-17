using System.Diagnostics.Eventing.Reader;

namespace StorageProductConnector.Abstractions
{
    public interface IStorageProductConnectorService
    {
        Task<int> AddProductOnStorage(int productId, int storageId);
        Task<int> RemoveProductFromStorage(int productId, int storageId);
        IEnumerable<int> GetProductsId(int storageId);
        IEnumerable<int> GetStoragesID(int productID);
        Task<bool> CheckStorage(int storageID);
        Task<bool> CheckProduct(int productID);
    }
}
