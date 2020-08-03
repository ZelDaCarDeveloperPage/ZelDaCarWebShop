using AutoShop.api.Intarface;
using System;

namespace AutoShop.api.Parse.PartnerRequestModels
{
    public class ForumAutoRequestModel : IRequestData
    {
        public string Login { get; set; } //апи логин
        public string Password { get; set; } //апи пароль

        public string Art { get; set; } // артикул запчасти
        public int Cross { get; set; } // 1-вернуть аналоги 0-нет
        public string Br { get; set; } // название бренда
        public string Gid { get; set; } // поиск толко по id (необяз)
    }
}