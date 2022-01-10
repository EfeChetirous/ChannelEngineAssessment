using ChannelEngine.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngine.Models.Models
{
    public class ApiRequestModel<T>
    {
        public HttpVerbs HttpVerb { get; set; }
        public bool RequiresToken { get; set; }
        public T RequestContent { get; set; }
    }
}
