using AutoShop.api;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using AutoShop.Models;
using System.Web;
using AutoShop.Models.ItemModels.Request;
using AutoShop.Models.ItemModels.Response;

namespace AutoShop.Controllers
{
    [Route("Item")]
    public class ItemController : Controller
    {
        [Route("GetListParam")]
        public JsonResult GetListParam(GetPartnerItemListRequestModel request) 
        {
            request.Brand = "";
            ForumAutoDataReader forumAutoDataReader = new ForumAutoDataReader();
            var res1 = forumAutoDataReader.GetListOfItem(new api.Parse.PartnerRequestModels.ForumAutoRequestModel()
            {
                Art = request.Id,
                Br = "",
                Cross = request.IsOnlyOriginal? 0 : 1,
                Gid = "",
                Login = "",
                Password = ""
            });
            GetPartnerItemListResponseModel response = new GetPartnerItemListResponseModel();
            if (res1 != null) response.PartnerItemList = res1.ToList();
            else response.PartnerItemList = new List<Item>();
            return Json(response);
        }

        [HttpGet]
        [Route("UserCase")]
        public ActionResult UserCase() 
        {
            //string req = @"https://partsapi.ru/api.php?act=getPartsbyVIN&vin=XW7BF4FK60S145161&type=oem&cat=&key=63376d365d6ccac73bac234517e0bb42";
            //HttpWebRequest requestClient = (HttpWebRequest)WebRequest.Create(req);
            //HttpWebResponse rs = (HttpWebResponse)requestClient.GetResponse();
            //using (StreamReader sr = new StreamReader(rs.GetResponseStream(), Encoding.UTF8, true))
            //{
            //    string data = sr.ReadToEnd();
            //}

            List<Item> response = new List<Item>();
            for (int i = 0; i < HttpContext.Request.Cookies.Count; i++)
            {
                HttpCookie cookie = HttpContext.Request.Cookies[i];
                if (!cookie.Name.Contains("Case")) continue;
                string cookieData = cookie.Value;
                Item item = new Item();
                string[] components = cookieData.Split('-');
                item.PRICE = int.Parse(components[0]);
                item.COUNT = int.Parse(components[1]);
                item.MANUFACTURER = components[2];
                item.EndDate = components[3];
                // перевод текущей кадировки в utf8 (для отображения кириллицы)
                item.NAME = Encoding.UTF8.GetString(Encoding.Default.GetBytes(components[4]));
                item.GID = components[5];
                response.Add(item);
            }
            return View(response);
        }

        [HttpPost]
        [Route("AddUserCase")]
        public void AddUserCase(Item usercase) 
        {
            int caseIndex = HttpContext.Request.Cookies.Count;
            HttpCookie cookie = new HttpCookie("newCase" + caseIndex);
            cookie.Value = usercase.ToString();
            HttpContext.Response.Cookies.Add(cookie);
        }



        // GET: Item
        [Route("GetList")]
        public ActionResult GetList()
        {
            string result = null;
            return View(result);
        }
    }
}