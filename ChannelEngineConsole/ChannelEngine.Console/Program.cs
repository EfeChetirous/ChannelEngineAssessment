using ChannelEngine.Common;
using ChannelEngine.Common.Interfaces;
using ChannelEngine.Services;
using ChannelEngine.Services.Intefaces;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ChannelEngine.Console
{

    class Program
    {
        private static IOrderService _orderService;
        private static IProductService _productService;
        private static IRestApiCore<object> _restApiCore;
        static void Main(string[] args)
        {
            var serviceProvider = initDI();
            _orderService = serviceProvider.GetService<IOrderService>();
            _productService = serviceProvider.GetService<IProductService>();
            _restApiCore = serviceProvider.GetService<IRestApiCore<object>>();

            drawMenu();
            

        }

        private static void drawMenu()
        {
            System.Console.Clear();
            System.Console.WriteLine("------ Options ------");
            System.Console.WriteLine("1 - Get All Orders By InProgress status ");
            System.Console.WriteLine("2 - Get Top 5 Products according to quantity ");
            System.Console.WriteLine("3 - Update Stocks");
            System.Console.WriteLine("3 - Quit");
        }

        private static ServiceProvider initDI()
        {
            //DI init
            var serviceCollection = new ServiceCollection()
                    .AddScoped<IOrderService, OrderService>()
                    .AddScoped<IProductService, ProductService>()
                    .BuildServiceProvider();
            return serviceCollection;
        }

    }
}
