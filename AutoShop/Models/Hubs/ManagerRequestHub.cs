//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web;
//using Microsoft.AspNet.SignalR;

//namespace AutoShop.Models.Hubs
//{
//    public class ManagerRequestHub : Hub
//    {
//        public List<User> Managers { get; set; }

//        public void SendNewRequestMessage()
//        {
//            Clients.All.hello();
//        }

//        public void Connect()
//        {
//            var id = int.Parse(Context.ConnectionId);

//            if (!Managers.Any(x => x.ID == id))
//            {
//                Managers.Add(new User { ID = id, FNAME = "Manager", SNAME = "" });

//                // Посылаем сообщение текущему пользователю
//                Clients.Caller.onConnected(id, Managers);

//                // Посылаем сообщение всем пользователям, кроме текущего
//                Clients.AllExcept(id.ToString()).onNewUserConnected(id);
//            }
//        }
//        public override Task OnDisconnected(bool stopCalled)
//        {
//            var item = Managers.FirstOrDefault(x => x.ID.ToString() == Context.ConnectionId);

//            if (item != null) 
//            {
//                Managers.Remove(item);
//                Clients.All.onUserDisconnected(Context.ConnectionId);
//            }
//            return base.OnDisconnected(stopCalled);
//        }
//    }
//}