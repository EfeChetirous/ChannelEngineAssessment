using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngine.Models.Models
{
    public class ApiResultModel<T> where T : class
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public string Code { get; set; }

        public T ResponseData { get; set; }

        public override string ToString()
        {
            return $"Code {Code}: , Message : {Message}";
        }
    }
}
