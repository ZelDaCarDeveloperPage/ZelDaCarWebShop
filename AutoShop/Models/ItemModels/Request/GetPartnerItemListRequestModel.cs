using System;

namespace AutoShop.Models.ItemModels.Request
{
    public class GetPartnerItemListRequestModel
    {
        public string Id { get; set; }
        public string Brand { get; set; }
        public bool IsByVin { get; set; }
        public bool IsOnlyOriginal { get; set; }
    }
}