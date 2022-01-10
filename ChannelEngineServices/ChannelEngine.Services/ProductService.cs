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
            ApiResultModel<ProductResponseModel> response = new ApiResultModel<ProductResponseModel>();

            try
            {
                ApiRequestModel<object> apiRequestModel = new ApiRequestModel<object>();
                apiRequestModel.HttpVerb = Common.Enums.HttpVerbs.Post;
                apiRequestModel.RequiresToken = true;
                apiRequestModel.ActionName = "Products";
                apiRequestModel.RequestContent = requestModel;
                var responseData = await _restApiCaller.SendRequest(apiRequestModel);
                response = await responseData.ToReturnModelAsync<ProductResponseModel>();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error has been occurred.";
            }            
            return response;
        }
    }
}
