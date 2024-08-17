using ASP.NET_Seminar_3.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Products_and_Storages_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;

        [HttpGet("CheckProduct/{productID}")]
        public ActionResult<bool> CheckProduct(int productID)
        {
            return _productService.CheckProduct(productID);
        }
    }
}
