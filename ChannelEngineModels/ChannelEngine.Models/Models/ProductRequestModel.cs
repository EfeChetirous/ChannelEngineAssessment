using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngine.Models.Models
{
    public class ProductRequestModel
    {
        public string MerchantProductNo { get; set; }
        public int Stock { get; set; } = 25;
    }
}
