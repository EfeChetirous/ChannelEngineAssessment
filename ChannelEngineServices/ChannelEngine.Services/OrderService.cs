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
        private readonly IRestApiCore<object> _restApiCaller;
        public OrderService(IRestApiCore<object> restApiCaller)
        {
            _restApiCaller = restApiCaller;
        }

        public async Task<ApiResultModel<OrderCollectionsModel>> GetAllOrdersByStatusType(string statusType = "IN_PROGRESS")
        {
            ApiRequestModel<object> requestModel = new ApiRequestModel<object>();
            requestModel.HttpVerb = Common.Enums.HttpVerbs.Get;
            requestModel.ActionName = "orders";
            requestModel.RequestContent = $"statuses={statusType}";
            requestModel.RequiresToken = true;
            var response = await _restApiCaller.SendRequest(requestModel);
            var data = await response.ToReturnModelAsync<OrderCollectionsModel>();
            return data;
        }

        public async Task<ApiResultModel<List<OrderLineModel>>> GetOrdersAccordingToQuantity(int count = 5)
        {
            ApiResultModel<List<OrderLineModel>> orderLines = new ApiResultModel<List<OrderLineModel>>();
            ApiResultModel<OrderCollectionsModel> orderCollection = await GetAllOrdersByStatusType();
            if (orderCollection.ResponseData.Content.Any())
            {
                List<OrderLineModel> topSoldOrders = orderCollection.ResponseData.Content.SelectMany(order => order.Lines)
                    .GroupBy(orderLine => orderLine.MerchantProductNo)
                    .Select(orderGroup => new OrderLineModel
                    {
                        ProductNo = orderGroup.First().MerchantProductNo,
                        Gtin = orderGroup.First().Gtin,
                        ProductDescription = orderGroup.First().Description,
                        Quantity = orderGroup.Sum(l => l.Quantity),
                    }).OrderByDescending(srt => srt.Quantity)
                    .Take(count)
                    .ToList();
                orderLines.Success = true;
                orderLines.ResponseData = topSoldOrders;
                return orderLines;
            }
            return orderLines;
        }
    }
}
