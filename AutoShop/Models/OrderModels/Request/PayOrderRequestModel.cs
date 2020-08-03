using System;

namespace AutoShop.Models.OrderModels.Request
{
    public class PayOrderRequestModel
    {
        public string UserMessage { get; set; }
        public int UserId { get; set; }
        public int GID { get; set; } // id товара в системе
        public string Partner { get; set; }
    }
}