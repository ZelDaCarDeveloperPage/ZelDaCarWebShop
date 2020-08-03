using System;

namespace AutoShop.api.Payment.YandexMoney
{
    public class YandexOrderResponseModel
    {
        public string OrderId { get; set; }
        public decimal Sum { get; set; }
    }
}