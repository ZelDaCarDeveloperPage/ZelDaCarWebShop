using System;
using System.Collections.Generic;
namespace AutoShop.api.Parse
{
    interface IDacarParser<T>
    {
        IEnumerable<T> getList(string data);
    }
}
