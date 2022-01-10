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
        Task<ApiResultModel<OrderCollectionsModel>> GetAllOrdersByStatusType(string statusType = "IN_PROGRESS");
        Task<IEnumerable<OrderLineModel>> GetOrdersAccordingToQuantity(int count = 5);
    }
}
