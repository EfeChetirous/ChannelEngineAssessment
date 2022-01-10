using ChannelEngine.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngine.Services.Intefaces
{
    public interface IProductService
    {
        Task<ApiResultModel<ProductResponseModel>> UpdateProductAsync(List<ProductRequestModel> requestModel);
    }
}
