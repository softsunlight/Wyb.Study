using Microsoft.AspNetCore.Mvc;
using Shop.Models.Domain.Request.Shop;
using Shop.Models.Domain.Response;
using Shop.Models.Domain.Response.Shop;
using Shop.Service.interfaces;

namespace Shop.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpPost]
        public Task<PageResponse<ShopListQueryItem>> GetListAsync(ShopListQueryRequest shopListQueyRequest)
        {
            return _shopService.GetListAsync(shopListQueyRequest);
        }
    }
}