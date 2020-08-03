using System;
using System.Collections.Generic;
namespace AutoShop.api
{
    interface IDataReader<ResponseModel, RequestModel>
    {
        IEnumerable<ResponseModel> GetListOfItem(RequestModel request);
    }
}
