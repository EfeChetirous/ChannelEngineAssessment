using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngine.Models.UIModels
{
    public class OrderLineModel
    {
        public string ProductNo { get; set; }
        public string ProductDescription { get; set; }
        public string Gtin { get; set; }
        public int Quantity { get; set; }
    }
}
