using System;

namespace AutoShop.api.Parse.PartnerResponseModels
{
    public class ForumAutoResponseListModel
    {
        public string Gid { get; set; } // id детали
        public string Brand { get; set; } // Бренд детали
        public string Art { get; set; } // Артикул детали
        public string Name { get; set; } // Имя детали
        public int D_deliv { get; set; } // дней доставки
        public int Kr { get; set; } // кол-во заказ товара должно быть кратно
        public int Num { get; set; } // доступно товара
        public float Price { get; set; } // Цена
        public string Whse { get; set; } // склад 
    }
}