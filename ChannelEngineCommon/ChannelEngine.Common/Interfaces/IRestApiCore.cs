using ChannelEngine.Common.Enums;
using ChannelEngine.Models.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngine.Common.Interfaces
{
    public interface IRestApiCore<T>
    {
        Task<HttpResponseMessage> SendRequest(ApiRequestModel<T> requestModel);
    }
}
