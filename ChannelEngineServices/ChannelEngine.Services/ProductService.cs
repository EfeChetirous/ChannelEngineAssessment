using ChannelEngine.Common.Extensions;
using ChannelEngine.Common.Interfaces;
using ChannelEngine.Models.Models;
using ChannelEngine.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngine.Services
{
    public class ProductService: IProductService
    {
        private readonly IRestApiCore<object> _restApiCaller;
        public ProductService(IRestApiCore<object> restApiCaller)
        {
            _restApiCaller = restApiCaller;
        }

        public async Task<ApiResultModel<ProductResponseModel>> UpdateProductAsync(List<ProductRequestModel> requestModel)
        { 
            ApiRequestModel<object> apiRequestModel = new ApiRequestModel<object>();
            apiRequestModel.HttpVerb = Common.Enums.HttpVerbs.Post;
            apiRequestModel.RequiresToken = true;
            apiRequestModel.ActionName = "Products";
            apiRequestModel.RequestContent = requestModel;
            var response = await _restApiCaller.SendRequest(apiRequestModel);
            var data = await response.ToReturnModelAsync<ProductResponseModel>();
            return data;
        }
    }
}
