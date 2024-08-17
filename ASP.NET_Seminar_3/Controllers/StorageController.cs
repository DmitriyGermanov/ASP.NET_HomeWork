using ASP.NET_Seminar_3.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Products_and_Storages_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StorageController(IStorageService storageService) : ControllerBase
    {
        private readonly IStorageService _storageService = storageService;

        [HttpGet("CheckStorage/{storageID}")]
        public ActionResult<bool> CheckStorage(int storageID)
        {
            return _storageService.CheckStorage(storageID);
        }
    }
}
