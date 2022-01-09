using ChannelEngine.Common.Extensions;
using ChannelEngine.Common.Interfaces;
using ChannelEngine.Models.Models;
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
        private readonly IRestApiCore<string> _fetchOrdersApi;
        public OrderService(IRestApiCore<string> fetchOrdersApi)
        {
            _fetchOrdersApi = fetchOrdersApi;
        }

        public async Task<ApiResultModel<OrderCollectionsModel>> GetAllOrders()
        {
            ApiRequestModel<string> requestModel = new ApiRequestModel<string>();
            requestModel.HttpVerb = Common.Enums.HttpVerbs.Get;
            requestModel.RequestContent = "orders?statuses=IN_PROGRESS";
            var response = await _fetchOrdersApi.SendRequest(requestModel);
            var data = response.ToReturnModel<OrderCollectionsModel>(); 
            //JsonConvert.DeserializeObject<OrderCollectionsModel>(restResponse.Content.ReadAsStringAsync().Result);
            return data;
        }
    }
}
