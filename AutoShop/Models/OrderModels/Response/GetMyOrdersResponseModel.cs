using System;
using System.Collections.Generic;
namespace AutoShop.Models.OrderModels.Response
{
    public class GetMyOrdersResponseModel
    {
        public List<Order> Orders { get; set; }
    }
}