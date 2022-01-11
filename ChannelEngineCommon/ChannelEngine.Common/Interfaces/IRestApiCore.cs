using ChannelEngine.Models.Models;

namespace ChannelEngine.Common.Interfaces
{
    public interface IRestApiCore<T>
    {
        Task<HttpResponseMessage> SendRequest(ApiRequestModel<T> requestModel);
    }
}
