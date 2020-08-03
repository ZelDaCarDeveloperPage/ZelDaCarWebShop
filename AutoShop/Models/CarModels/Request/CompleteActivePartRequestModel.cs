using System;
using System.Collections.Generic;

namespace AutoShop.Models.CarModels.Request
{
    public class CompleteActivePartRequestModel
    {
        public int UserId { get; set; }
        public string VIN { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public PartModel[] Arts { get; set; }

    }
}