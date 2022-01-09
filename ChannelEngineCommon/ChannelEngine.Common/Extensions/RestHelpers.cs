﻿using ChannelEngine.Models.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngine.Common.Extensions
{
    public static class RestHelpers
    {
        public static ApiResultModel<T> ToReturnModel<T>(this HttpResponseMessage response) where T : class
        {
            ApiResultModel<T> returnModel;
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

            var result = response.Content.ReadAsStringAsync().Result;

            try
            {
                returnModel = JsonConvert.DeserializeObject<ApiResultModel<T>>(result);
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