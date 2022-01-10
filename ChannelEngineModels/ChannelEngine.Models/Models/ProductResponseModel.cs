using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngine.Models.Models
{
    public class ProductContent
    {
        public int RejectedCount { get; set; }
        public int AcceptedCount { get; set; }
    }

    public class ProductResponseModel
    {
        public ProductContent Content { get; set; }
        public int StatusCode { get; set; }
        public object LogId { get; set; }
        public bool Success { get; set; }
        public object Message { get; set; }
    }

}
