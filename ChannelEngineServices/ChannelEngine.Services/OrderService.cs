using ChannelEngine.Common.Extensions;
using ChannelEngine.Common.Interfaces;
using ChannelEngine.Models.Models;
using ChannelEngine.Models.UIModels;
using ChannelEngine.Services.Intefaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngine.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRestApiCore<object> _fetchApi;
        public OrderService(IRestApiCore<object> fetchApi)
        {
            _fetchApi = fetchApi;
        }

        public async Task<ApiResultModel<OrderCollectionsModel>> GetAllOrdersByStatusType(string statusType = "IN_PROGRESS")
        {
            ApiRequestModel<object> requestModel = new ApiRequestModel<object>();
            requestModel.HttpVerb = Common.Enums.HttpVerbs.Get;
            requestModel.RequestContent = $"orders?statuses={statusType}";
            requestModel.RequiresToken = true;
            var response = await _fetchApi.SendRequest(requestModel);
            var data = await response.ToReturnModelAsync<OrderCollectionsModel>();
            return data;
        }

        public async Task<IEnumerable<OrderLineModel>> GetOrdersAccordingToQuantity(int count = 5)
        {
            ApiResultModel<OrderCollectionsModel> orderCollection = await GetAllOrdersByStatusType();
            if (orderCollection.ResponseData.Content.Any())
            {
                IEnumerable<OrderLineModel> topSoldOrders = orderCollection.ResponseData.Content.SelectMany(order => order.Lines)
                    .GroupBy(orderLine => orderLine.MerchantProductNo)
                    .Select(orderGroup => new OrderLineModel
                    {
                        MerchantProductNo = orderGroup.First().MerchantProductNo,
                        Gtin = orderGroup.First().Gtin,
                        ProductDescription = orderGroup.First().Description,
                        Quantity = orderGroup.Sum(l => l.Quantity),
                    }).OrderByDescending(srt => srt.Quantity).Take(5);
                return topSoldOrders;
            }
            return new List<OrderLineModel>();
        }
    }
}
