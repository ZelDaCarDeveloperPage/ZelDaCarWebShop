using System;

namespace AutoShop.Models.UserModels.Request
{
    public class SetUserPartArtsRequestModel
    {
        public string VinCode { get; set; }
        public string[] Parts { get; set; }
    }
}