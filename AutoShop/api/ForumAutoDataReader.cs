using AutoShop.api.Parse;
using AutoShop.api.Parse.PartnerRequestModels;
using AutoShop.api.Parse.PartnerResponseModels;
using AutoShop.Models;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System;

namespace AutoShop.api
{
    public class ForumAutoDataReader : IDataReader<Item, ForumAutoRequestModel>
    {
        public IEnumerable<Item> GetListOfItem(ForumAutoRequestModel request)
        {
            request.Login = "3165_bakulov";
            request.Password = "GUWTWAQCFd";

            request.Gid = null;
            string requestString = $"https://api.forum-auto.ru/v2/listGoods?login={request.Login}&pass={request.Password}";
            requestString += $"&art={request.Art}";
            requestString += $"&cross={request.Cross}";
            if (request.Br != null) requestString += $"&br={request.Br}";
            if (request.Gid != null) requestString += $"&gid={request.Gid}";

            HttpWebRequest requestClient = (HttpWebRequest)WebRequest.Create(requestString);
            HttpWebResponse response = (HttpWebResponse)requestClient.GetResponse();
            using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8, true))
            {
                JsonDacarParser<ForumAutoResponseListModel> DacarParser = new Parse.JsonDacarParser<ForumAutoResponseListModel>();
                string data = sr.ReadToEnd();
                IEnumerable<ForumAutoResponseListModel> responselist = DacarParser.getList(data);
                DateTime endDate = DateTime.Now;
                if (responselist == null) return null;
                else return (from item in responselist
                             select new Item()
                             {
                                 NAME = item.Name,
                                 PRICE = (int)item.Price,
                                 COUNT = item.Num,
                                 EndDate = endDate.AddDays(item.D_deliv + 1).ToShortDateString(),
                                 MANUFACTURER = item.Brand,
                                 GID = item.Gid
                             });
            }
        }
    }
}