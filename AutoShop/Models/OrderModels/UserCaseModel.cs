using System;

namespace AutoShop.Models.OrderModels
{
    public class UserCaseModel
    {
        public int UserId { get; set; }
        public Order Order { get; set; }
    }
}