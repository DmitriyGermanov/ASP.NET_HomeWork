using ASP.NET_Seminar_3.Abstractions;
using ASP.NET_Seminar3.Models;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace ASP.NET_Seminar_3.Services
{
    public class StorageService(Seminar3Context dbContext, IMapper mapper, IMemoryCache cache) : IStorageService
    {
        private readonly Seminar3Context _context = dbContext;
        private readonly IMapper _mapper = mapper;
        private readonly IMemoryCache _cache = cache;
        public int AddStorage(StorageDto storageDto)
        {
            var entityStorage = _context.Storages.FirstOrDefault(cat => cat.Name != null
                      && cat.Name.Equals(storageDto.Name, StringComparison.OrdinalIgnoreCase));
            if (entityStorage == null)
            {
                entityStorage = _mapper?.Map<Storage>(storageDto) ?? throw new Exception("Adding storage can't be Null.");

                _context.Storages.Add(entityStorage);
                _context.SaveChanges();
            }

            var entity = _mapper.Map<StorageDto>(storageDto);
            _context.Add(entity);
            _context.SaveChanges();
            _cache.Remove("storages");
            return entity.Id;
        }

        public IEnumerable<StorageDto> GetStorages()
        {

            if (_cache.TryGetValue("storages", out List<StorageDto>? storages) && storages != null)
                return storages;

            storages = [.. _context.Storages.Select(storage => _mapper.Map<StorageDto>(storage))];

            _cache.Set("storages", storages, TimeSpan.FromMinutes(30));

            return storages;
        }
    }
}
