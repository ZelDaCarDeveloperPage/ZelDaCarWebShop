using AutoShop.Models;
using AutoShop.Models.OrderModels.Request;
using AutoShop.Models.OrderModels.Response;
using AutoShop.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoShop.Controllers
{
    [Route("Order")]
    public class OrderController : Controller
    {
        private readonly OrderRepository _orderRepository;
        private readonly PayOrderRepository _payOrderRepository;
        public OrderController() 
        {
            _orderRepository = new OrderRepository();
            _payOrderRepository = new PayOrderRepository();
        }

        // GET: Order
        [Route("ShowAllOrders")]
        public ActionResult ShowAllOrders()
        {
            List<Order> allOrders = _orderRepository.GetList().ToList();
            return View(allOrders);
        }

        [Route("PayOrder")]
        public JsonResult PayOrder(PayOrderRequestModel request) 
        {
            PayOrderResponseModel response = new PayOrderResponseModel();
            
            response.Message = "Заказ оформлен";
            return Json(response);
        }

        [Route("ShowMyOrders")]
        public JsonResult ShowMyOrders(GetMyOrdersRequestModel request)
        {
            GetMyOrdersResponseModel response = new GetMyOrdersResponseModel();
            response.Orders = _orderRepository.GetMyOrders(request.UserId);
            return Json(response);
        }

        /// <summary>
        /// ONLY FOR ADMINS 
        /// </summary>
        /// <returns>UPDATE STATE JSON</returns>
        [Route("UpdatePartnerItemsInfo")]
        public JsonResult UpdatePartnerItemsInfo()
        {
            JsonResult response = null;
            return response;
        }

        /// <summary>
        /// ONLY FOR ADMINS
        /// </summary>
        /// <returns>UPDATE STATE JSON</returns>
        [Route("UpdateOrderInfo")]
        public JsonResult UpdateOrderInfo(Order order)
        {
            JsonResult response = null;
            return response;
        }
    }
}