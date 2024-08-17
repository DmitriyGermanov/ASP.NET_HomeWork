using ASP.NET_Seminar3.Models;

namespace ASP.NET_Seminar_3.Abstractions
{
    public interface IStorageService
    {
        IEnumerable<StorageDto> GetStorages();
        int AddStorage(StorageDto storageDto);
        bool CheckStorage(int storageID);
    }
}
