using ChannelEngine.Models.Models;
using System.Collections.Generic;

namespace ChannelEngine.Tests
{
    public class RandomDataGenerator
    {
        public static ApiResultModel<OrderCollectionsModel> GenerateData()
        {
            
            var productFirst = new Line
            {
                Description = "skirt yellow",
                Quantity = 21,
                MerchantProductNo = "001201-S",
                Gtin = "111111111111111",
            };

            var productSecond = new Line
            {
                Description = "towel",
                Quantity = 22,
                MerchantProductNo = "001401-S",
                Gtin = "444444444",
            };

            var productThird = new Line
            {
                Description = "jean",
                Quantity = 14,
                MerchantProductNo = "001501-S",
                Gtin = "3333333333",
            };

            var productFourth = new Line
            {
                Description = "tshirt black",
                Quantity = 9,
                MerchantProductNo = "002201-M",
                Gtin = "334233453453",
            };

            var productFifth = new Line
            {
                Description = "tshirt",
                Quantity = 7,
                MerchantProductNo = "021201-S",
                Gtin = "342445546",
            };

            var productSixth = new Line
            {
                Description = "pant",
                Quantity = 6,
                MerchantProductNo = "401201-S",
                Gtin = "2343222222",
            };

            var lines = new List<ChannelEngine.Models.Models.Line>();
            lines.Add(productFirst);
            lines.Add(productSecond);
            lines.Add(productThird);

            var lines2 = new List<ChannelEngine.Models.Models.Line>();
            lines2.Add(productFourth);
            lines2.Add(productFifth);
            lines2.Add(productSixth);

            var content2 = new Content() { Lines = lines2 };
            List<Content> contents = new List<Content>();
            contents.Add(content2);

            var content = new Content() { Lines = lines };
            contents.Add(content);

            OrderCollectionsModel orderCollection = new OrderCollectionsModel();
            orderCollection.Content = contents;
            
            var response = new ApiResultModel<OrderCollectionsModel>
            {
                Code = "OK",
                ResponseData = orderCollection,
                Success = true
            };
            return response;
        }
    }
}
