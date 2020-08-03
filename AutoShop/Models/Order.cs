using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoShop.Models
{
    public class Order
    {
        public int ID { get; set; }
        public int USER_ID { get; set; }
        
        /// <summary>
        /// Serialize info (MODEL ITEM)
        /// </summary>
        public string ITEMNAME { get; set; }
        public int SUM { get; set; }
        public DateTime ENDDATE { get; set; }
        public string MAKER { get; set; }
        public override string ToString()
        {
            return USER_ID + "-" + ITEMNAME + "-" + SUM + "-" 
                + ENDDATE.ToShortDateString() + "-" + MAKER;
        }
    }
}