using Microsoft.AspNetCore.Mvc;
using StorageProductConnector.Abstractions;

namespace StorageProductConnector.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductStorageConnectorController(IStorageProductConnectorService storageProductConnectorService) : ControllerBase
    {
        private readonly IStorageProductConnectorService _storageProductConnectorService = storageProductConnectorService;
        [HttpGet("check_product/{productID}")]
        public async Task<ActionResult<bool>> CheckProduct(int productID)
        {
            try
            {
                if (await _storageProductConnectorService.CheckProduct(productID))
                    return Ok(productID);
                else
                    return NotFound("Product Not Found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpGet("check_storage/{storageId}")]
        public async Task<ActionResult<bool>> CheckStorage(int storageId)
        {
            try
            {
                if (await _storageProductConnectorService.CheckStorage(storageId))
                    return Ok(storageId);
                else
                    return NotFound("Storage Not Found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpPost("check_storage/{productID}/{storageId}")]
        public async Task<ActionResult<int>> AddProductOnStorage(int productID, int storageId)
        {
            try
            {
                return Ok(await _storageProductConnectorService.AddProductOnStorage(productID, storageId));
            }
            catch (ArgumentException ex)
            { return NotFound(ex.Message); }
            catch (Exception ex)
            { return StatusCode(500, ex); }
        }
        [HttpGet("get_products/{storageId}")]
        public ActionResult<IEnumerable<int>> GetProducts(int storageId)
        {
            try
            {
               return Ok(_storageProductConnectorService.GetProductsId(storageId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpGet("get_storages/{productId}")]
        public ActionResult<IEnumerable<int>> GetStorages(int productId)
        {
            try
            {
                return Ok(_storageProductConnectorService.GetStoragesID(productId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
