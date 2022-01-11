using ChannelEngine.Models.Models;
using ChannelEngine.Models.UIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngine.Services.Intefaces
{
    public interface IOrderService
    {
        public Task<ApiResultModel<OrderCollectionsModel>> GetAllOrdersByStatusType(string statusType = "IN_PROGRESS");
        public Task<ApiResultModel<List<OrderLineModel>>> GetOrdersAccordingToQuantity(int count = 5);
        ApiResultModel<List<OrderLineModel>> FetchOrderData(ApiResultModel<OrderCollectionsModel> orderCollection, int count = 5);
    }
}
