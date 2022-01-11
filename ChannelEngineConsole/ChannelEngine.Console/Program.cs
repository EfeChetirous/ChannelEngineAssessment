using ChannelEngine.Common;
using ChannelEngine.Common.Interfaces;
using ChannelEngine.Services;
using ChannelEngine.Services.Intefaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ChannelEngine.Models.Enums;
using ChannelEngine.Models.Models;

namespace ChannelEngine.Console
{

    class Program
    {
        private static IOrderService _orderService;
        private static IProductService _productService;
        private static IRestApiCore<object> _restApiCore;
        private readonly IConfiguration _configuration;
        static async Task Main(string[] args)
        {
            var serviceProvider = initDI();
            _restApiCore = serviceProvider.GetService<IRestApiCore<object>>();
            _orderService = serviceProvider.GetService<IOrderService>();
            _productService = serviceProvider.GetService<IProductService>();

            System.Console.Clear();
            while (true)
            {
                drawMenu();
                var selectedOption = Convert.ToInt32(System.Console.ReadLine());

                switch (selectedOption)
                {
                    case (int)MenuSelection.InProgress:
                        await GetInProgressOrders();
                        break;
                    case (int)MenuSelection.TopFive:
                        await GetTopFiveOrders();
                        break;
                    case (int)MenuSelection.UpdateStocks:
                        await UpdateStock();
                        break;
                    default:
                        System.Console.WriteLine("Invalid selection.. Please select agein");
                        break;
                }
            }

        }

        private static async Task UpdateStock()
        {
            await GetTopFiveOrders();
            System.Console.WriteLine("Please type the product no in order to update stocks to 25");
            string productNo = System.Console.ReadLine();
            List<ProductRequestModel> productRequestModel = new List<ProductRequestModel>();
            productRequestModel.Add(new ProductRequestModel() { MerchantProductNo = productNo, Stock = 25 });
            var result = await _productService.UpdateProductAsync(productRequestModel);
            if (result.Success)
            {
                System.Console.WriteLine("the product stock updated to 25");
            }
            else
            {
                System.Console.WriteLine("An error has been occured");
            }
        }

        private static async Task GetTopFiveOrders()
        {
            var topFiveOrders = await _orderService.GetOrdersAccordingToQuantity(5);
            if (topFiveOrders.Success)
            {
                foreach (var order in topFiveOrders.ResponseData)
                {
                    System.Console.WriteLine($"ProductDescription : {order.ProductDescription} Quantity : {order.Quantity} ProductNo : {order.ProductNo} Gtin : {order.Gtin}");
                }
            }
            else
            {
                System.Console.WriteLine("An error has ben occured");
            }
        }

        private static async Task GetInProgressOrders()
        {
            var inprogressOrders = await _orderService.GetAllOrdersByStatusType();
            if (inprogressOrders.Success)
            {
                foreach (var order in inprogressOrders.ResponseData.Content)
                {
                    System.Console.WriteLine($"ChannelName : {order.ChannelName} ChannelOrderNo : {order.ChannelOrderNo} ChannelCustomerNo : {order.ChannelCustomerNo} Status : {order.Status}");
                }
            }
            else
            {
                System.Console.WriteLine("An error has ben occured");
            }
        }

        private static void drawMenu()
        {
            System.Console.WriteLine("------ Options ------");
            System.Console.WriteLine("1 - Get All Orders By InProgress status ");
            System.Console.WriteLine("2 - Get Top 5 Products according to quantity ");
            System.Console.WriteLine("3 - Update Stocks");
            System.Console.WriteLine("4 - Quit");
        }

        private static ServiceProvider initDI()
        {
            //DI init
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            IConfiguration config = builder.Build();

            var serviceCollection = new ServiceCollection()
                    .AddScoped<IRestApiCore<object>, RestApiCore<object>>()
                    .AddScoped<IOrderService, OrderService>()
                    .AddScoped<IProductService, ProductService>()
                    .AddSingleton<IConfiguration>(config)
                    .BuildServiceProvider();
            return serviceCollection;
        }

    }
}
