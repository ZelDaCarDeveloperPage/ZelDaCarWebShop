using System;

namespace AutoShop.api.Intarface
{
    public interface IRequestData
    {
        string Login { get; set; }
        string Password { get; set; }
    }
}