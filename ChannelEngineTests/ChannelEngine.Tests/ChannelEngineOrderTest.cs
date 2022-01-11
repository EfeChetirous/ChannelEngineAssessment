using Xunit;
using Moq;
using System.Collections.Generic;
using ChannelEngine.Models.UIModels;
using ChannelEngine.Services;
using ChannelEngine.Common.Interfaces;
using ChannelEngine.Models.Models;
using System.Threading.Tasks;
using ChannelEngine.Services.Intefaces;

namespace ChannelEngine.Tests
{
    public class ChannelEngineOrderTest
    {
        private readonly IOrderService _orderService;
        private readonly Mock<IRestApiCore<object>> _mockRestApiCoreService;

        public ChannelEngineOrderTest()
        {
            _mockRestApiCoreService = new Mock<IRestApiCore<object>>();
            _orderService = new OrderService(_mockRestApiCoreService.Object);
        }

        [Fact]
        public void ShouldTheCountOfReturnRecordsBeFive()
        {
            //arrange
            var dummyData = RandomDataGenerator.GenerateData();
            //act
            var response =  _orderService.FetchOrderData(dummyData, 5);
            //assert
            Assert.Equal(5, response.ResponseData.Count);
        }

        [Fact]
        public void ShouldReturnItemsOrdered()
        {
            //arrange
            var dummyData = RandomDataGenerator.GenerateData();
            //act
            var response = _orderService.FetchOrderData(dummyData, 5);
            //assert
            Assert.Equal(9, response.ResponseData[3].Quantity);
            Assert.Equal(22, response.ResponseData[0].Quantity);
        }
    }
}