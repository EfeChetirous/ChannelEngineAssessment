using ChannelEngine.Models.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ChannelEngine.Common.Extensions
{
    public static class RestHelpers
    {
        public static async Task<ApiResultModel<T>> ToReturnModelAsync<T>(this HttpResponseMessage response) where T : class
        {
            ApiResultModel<T> returnModel = new ApiResultModel<T>();
            if (!response.IsSuccessStatusCode)
            {
                returnModel = new ApiResultModel<T>
                {
                    Success = false,
                    Code = ((int)response.StatusCode).ToString(),
                    Message = response.StatusCode.ToString()
                };

                return returnModel;
            }
            else
            {
                returnModel.Success = true;
                returnModel.Code = response.StatusCode.ToString();
            }

            var result = await response.Content.ReadAsStringAsync();

            try
            {
                returnModel.ResponseData = JsonConvert.DeserializeObject<T>(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var message = "Cannot deserialize response to ReturnModel: \n" + result;
                throw new Exception(message);
            }

            return returnModel;
        }

        public static HttpContent ToHttpContent(this object o)
        {
            var content = JsonConvert.SerializeObject(o);

            var buffer = Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return byteContent;
        }
    }
}
