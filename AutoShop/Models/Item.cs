using System;

namespace AutoShop.Models
{
    public class Item
    {
        public int PRICE { get; set; }
        public int COUNT { get; set; }
        public string MANUFACTURER { get; set; }
        public string EndDate { get; set; }
        public string NAME { get; set; }
        public string GID { get; set; }

        public override string ToString()
        {
            return PRICE + "-" + COUNT + "-" + MANUFACTURER +
                 "-" + EndDate + "-" + NAME + "-" + GID;
        }
    }
}