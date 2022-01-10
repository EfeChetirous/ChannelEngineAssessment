using ChannelEngine.Services.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace ChannelEngine.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var orders = await _orderService.GetAllOrdersByStatusType();
                return View(orders);
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
