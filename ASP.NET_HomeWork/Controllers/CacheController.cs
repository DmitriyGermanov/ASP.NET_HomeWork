using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace ASP.NET_HomeWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController(IMemoryCache memoryCache) : ControllerBase
    {
        private readonly IMemoryCache _cache = memoryCache;
        [HttpGet("get_cache_statistics")]
        public ActionResult<MemoryCacheStatistics> GetCacheStats()
        {
            return _cache.GetCurrentStatistics() ?? throw new NullReferenceException("Cache can't be null");
        }

        [HttpGet("get_cache_statistics_in_csv_url")]
        public ActionResult<string> GetCacheStatsInCsvUrl()
        {
            string filename = "cacheStat" + DateTime.Now.ToBinary().ToString() + ".csv";
            System.IO.File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles", filename), CacheController.FormatMemoryCacheStatistics(_cache.GetCurrentStatistics()));

            return "https://" + Request.Host.ToString() + "/static/" + filename;
        }
        static string FormatMemoryCacheStatistics(MemoryCacheStatistics? cache)
        {
            if (cache == null)
                return String.Empty;

            return $"currentEntryCount: {cache.CurrentEntryCount}, \n" +
                   $"currentEstimatedSize: {cache.CurrentEstimatedSize}, \n" +
                   $"totalMisses: {cache.TotalMisses}, \n" +
                   $"totalHits: {cache.TotalHits}";
        }
    }
}
