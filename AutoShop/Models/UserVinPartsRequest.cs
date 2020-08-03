using System;

namespace AutoShop.Models
{
    public class UserVinPartsRequest
    {
        public int ID { get; set; }
        public string Request { get; set; }
        public string VinCode { get; set; }
        public int UserID { get; set; }
        public int? ManagerId { get; set; }
        public bool IsCompleted { get; set; }
    }
}