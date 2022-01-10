using ChannelEngine.Common.Enums;
using ChannelEngine.Common.Extensions;
using ChannelEngine.Common.Interfaces;
using ChannelEngine.Models.Models;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngine.Common
{
    public class RestApiCore<T> : IRestApiCore<T> where T : class
    {
        private string _baseUrl;
        private readonly IConfiguration _configuration;
        private readonly string _apiKey;
        public RestApiCore(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseUrl = _configuration["AppSettings:ChannelEngineBaseUrl"];
            _apiKey = _configuration["AppSettings:ChannelEngineApiKey"];
        }


        public async Task<HttpResponseMessage> SendRequest(ApiRequestModel<T> requestModel)
        {
            HttpResponseMessage response = null;
            string url = _baseUrl;
            using (var client = new HttpClient { BaseAddress = new Uri(_baseUrl), Timeout = TimeSpan.FromSeconds(30) })
            {
                try
                {
                    switch (requestModel.HttpVerb)
                    {
                        case HttpVerbs.Post:
                            var postContent = requestModel.RequestContent.ToHttpContent();
                            response = await client.PostAsync(_baseUrl, postContent);
                            break;
                        case HttpVerbs.Get:
                            url += $"{requestModel.RequestContent}";
                            url += requestModel.RequiresToken ? $"&apiKey={_apiKey}" : "";
                            response = await client.GetAsync(url);
                            break;
                        case HttpVerbs.Delete:
                            response = await client.DeleteAsync(_baseUrl);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    response = null;
                }

                if (response == null)
                {
                    Console.WriteLine("Cannot get response from server.");
                    return null;
                }

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine($"Token is unauthorized to do this action: [{requestModel.HttpVerb.ToString().ToUpper()}] /{_baseUrl}. Please check your bearer token in request header.");
                }
                return response;
            }
        }

    }
}
