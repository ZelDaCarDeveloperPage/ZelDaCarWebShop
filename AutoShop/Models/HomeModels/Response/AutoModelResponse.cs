using System;
namespace AutoShop.Models.HomeModels.Response
{
    public class AutoModelResponse
    {
        public OrderRequestState State { get; set; }
        public string Message { get; set; }

        public string CarModel { get; set; }
        public string CarPower { get; set; }
    }
}