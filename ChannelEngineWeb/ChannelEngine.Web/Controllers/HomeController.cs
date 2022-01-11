using ChannelEngine.Models.Models;
using ChannelEngine.Services.Intefaces;
using ChannelEngine.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChannelEngine.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        public HomeController(ILogger<HomeController> logger, IOrderService orderService, IProductService productService)
        {
            _logger = logger;
            _orderService = orderService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}