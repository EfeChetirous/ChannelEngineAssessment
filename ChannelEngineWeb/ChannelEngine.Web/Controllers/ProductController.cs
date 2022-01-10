using ChannelEngine.Models.Models;
using ChannelEngine.Services.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace ChannelEngine.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        public ProductController(IProductService productService
            ,IOrderService orderService)
        {
            _productService = productService;
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var topSoldOrders = await _orderService.GetOrdersAccordingToQuantity(5);
            return View(topSoldOrders);
        }
        public async Task<IActionResult> UpdateStock(string productNo)
        {
            if (!string.IsNullOrWhiteSpace(productNo))
            {
                List<ProductRequestModel> productRequestModels = new List<ProductRequestModel>();
                var productRequestModel = new ProductRequestModel()
                {
                    MerchantProductNo = productNo,
                    Stock = 25
                };
                productRequestModels.Add(productRequestModel);
                var response = await _productService.UpdateProductAsync(productRequestModels);
                ViewBag.Response = response;
                return View("Index");
            }
            return View("Index");
        }
    }
}
