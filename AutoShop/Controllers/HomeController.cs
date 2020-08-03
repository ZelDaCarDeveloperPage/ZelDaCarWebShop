using AutoShop.Models.HomeModels.Request;
using AutoShop.Models.HomeModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoShop.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        [Route("Index")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("About")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Route("Contact")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Route("GetAutoModel")]
        public JsonResult GetAutoModel(AutoModelRequest request) 
        {
            //https://www.drive2.ru/b/1286924/
            AutoModelResponse response = new AutoModelResponse();
            if (request.Vin == null || request.Vin.Length == 0) 
            {
                response.Message = "Введите вин код!";
                response.State = Models.HomeModels.OrderRequestState.Fail;
                return Json(response);
            }
            if (request.Vin.Length != 17) 
            {
                response.Message = "Вин код должен состоять из 17 символов и цифр!";
                response.State = Models.HomeModels.OrderRequestState.Fail;
                return Json(response);
            }
            response.CarModel = "123123";
            response.CarPower = "1213";
            response.State = Models.HomeModels.OrderRequestState.Completed;
            return Json(response);
        }
    }
}