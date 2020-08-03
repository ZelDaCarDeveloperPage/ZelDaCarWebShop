using AutoShop.api.Excel;
using AutoShop.Models;
using AutoShop.Models.CarModels.Request;
using AutoShop.Models.CarModels.Response;
using AutoShop.Models.Repository;
using AutoShop.Models.UserModels.Request;
using AutoShop.Models.UserModels.Response;

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoShop.Controllers
{
    [Route("User")]
    public class UserController : Controller
    {
        private readonly UserRepository _userRepository;
        private readonly RoleRepository _roleRepository;
        private readonly Role_UserRepository _role_UserRepository;
        private readonly UserVinPartsRequestRepository _userVinPartsRequestRepository;
        private readonly UsersPartActRepository _usersPartActRepository;
        public UserController()
        {
            _userRepository = new UserRepository();
            _roleRepository = new RoleRepository();
            _role_UserRepository = new Role_UserRepository();
            _userVinPartsRequestRepository = new UserVinPartsRequestRepository();
            _usersPartActRepository = new UsersPartActRepository();
        }

        // GET: User
        [Route("Login")]
        public ActionResult Login() => View();

        [HttpPost]
        [Route("Login")]
        public ActionResult Login(string email, string password)
        {
            User curUser = null;
            if ((curUser = _userRepository.GetUserByEmailAndPass(email, password)) != null)
            {
                Role_User role_User = _role_UserRepository.GetByUserId(curUser.ID);
                Role role = _roleRepository.Get(role_User.ROLE_ID);
                switch (role.TYPE)
                {
                    case Models.Enum.RoleType.ADMIN:
                        Session["Role"] = "Admin";
                        break;
                    case Models.Enum.RoleType.CLIENT:
                        Session["Role"] = "Client";
                        break;
                    case Models.Enum.RoleType.PARTHNER:
                        Session["Role"] = "Partner";
                        break;
                    default:
                        Session["Role"] = "Undefined";
                        break;
                }
                Session["Authed"] = true;
                Session["UserId"] = curUser.ID;
                return Redirect($"/User/MyProfile?userId={curUser.ID}");
            }
            else
            {
                Session["Authed"] = false;
                return Redirect("/User/Register");
            }
        }

        [Route("LogOut")]
        public ActionResult LogOut() 
        {
            Session["Authed"] = false;
            Session["UserId"] = null;
            Session["Role"] = null;
            return RedirectToAction("Login");
        }
             

        [HttpGet]
        [Route("Register")]
        public ActionResult Register() => View();
        [HttpPost]
        [Route("Register")]
        public ActionResult Register(User user)
        {
            user = _userRepository.Create(user);
            int roleId = _roleRepository.FindRoleIdByType(Models.Enum.RoleType.CLIENT);
            _role_UserRepository.Create(new Role_User()
            {
                GIVERID = -1,
                ROLE_ID = roleId,
                USER_ID = user.ID
            });
            return Redirect("/User/Login");
        }

        [HttpGet]
        [Route("MyProfile")]
        public ActionResult MyProfile(int userId)
        {
            if ((int)Session["UserId"] != userId)
            {
                return View("ErrorLogin");
            }
            User curUser = _userRepository.GetUserById(userId);
            return View(curUser);
        }


        [Route("ShowUserList")]
        public ActionResult ShowUserList()
        {
            IEnumerable<User> users = _userRepository.GetList();
            return View(users);
        }

        [HttpGet]
        [Route("SendPartsRequestForArts")]
        public ActionResult SendPartsRequestForArts()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SendPartsRequestForArts(SetUserPartArtsRequestModel request)
        {
            SetUserPartArtsResponseModel response = new SetUserPartArtsResponseModel();
            if (Session["UserId"] == null) return Json("Необходимо авторизироваться");
            int userId = (int)Session["UserId"];
            if (_userVinPartsRequestRepository.GetActiveUserRequest(userId) != null) 
            {
                response.Message = "Вы уже имеете активный запрос";
                return Json(response);
            }
            if (request.Parts != null && request.Parts.Length > 0 && request.VinCode != null && request.VinCode.Length == 17)
            {
                UserVinPartsRequest model = new UserVinPartsRequest();
                model.UserID = userId;
                model.VinCode = request.VinCode;
                model.ManagerId = null;
                model.IsCompleted = false;
                model.Request = string.Join("-", request.Parts);
                _userVinPartsRequestRepository.Create(model);
                response.Message = "Запрос успешно добавлен. В скоро времени наши менеджеры определят номера запчастей.";
            }
            else
            {
                response.Message = "Неверные входные данные";
            }
            return Json(response);
        }

        [Route("ShowUserProfile")]
        public ActionResult ShowUserProfile(int userId)
        {
            return View();
        }

        [Route("ShowAdminPanel")]
        public ActionResult ShowAdminPanel()
        {
            return View();
        }

        [Route("GetActivePartRequests")]
        public JsonResult GetActivePartRequests(GetActivePartRequestsModel request) 
        {
            GetActivePartResponseModel response = new GetActivePartResponseModel();
            response.UserRequests = new List<Models.CarModels.UserCarArtParts>();
            List<UserVinPartsRequest> userRequests;
            if (request.IsActive) userRequests = _userVinPartsRequestRepository.GetActiveRequests();
            else userRequests = _userVinPartsRequestRepository.GetList().ToList();

            foreach (var req in userRequests) 
            {
                User user = _userRepository.Get(req.UserID);
                response.UserRequests.Add(new Models.CarModels.UserCarArtParts()
                {
                    UserName = user.FNAME + " " + user.SNAME,
                    UserId = user.ID,
                    RequestCount = req.Request.Split('-').Length
                }); 
            }
            return Json(response);
        }

        [Route("CompletePartRequest")]
        public ActionResult CompletePartRequest(int userid) 
        {
            UserVinPartsRequest request = _userVinPartsRequestRepository.GetActiveUserRequest(userid);
            User user = _userRepository.Get(userid);
            
            CompleteActivePartRequestModel model = new CompleteActivePartRequestModel();
            model.UserId = userid;
            model.Phone = user.TEL;
            model.Email = user.EMAIL;
            model.VIN = request.VinCode;
            string[] parts = request.Request.Split('-');
            model.Arts = new Models.CarModels.PartModel[parts.Length];
            for (int i = 0; i < parts.Length; i++) 
            {
                model.Arts[i] = new Models.CarModels.PartModel() { Title = parts[i], Value = "ID" };
            }
            return View(model);
        }

        [HttpPost]
        [Route("CompletePartRequest")]
        public ActionResult CompletePartRequest(CompleteActivePartRequestModel request)
        {
            UsersPartActs model = new UsersPartActs();
            model.UserId = request.UserId;
            model.Arts = "";
            int index = 0;
            foreach (var item in request.Arts) 
            {
                index++;
                model.Arts += $"{item.Title}-{item.Value}";
                if (index < request.Arts.Length) model.Arts += "|";
            }
            UsersPartActs parts;
            if ((parts = _usersPartActRepository.GetByUserId(request.UserId)) == null)
            {
                _usersPartActRepository.Create(model);
            }
            else 
            {
                parts.Arts += "|"; 
                parts.Arts += model.Arts;
                _usersPartActRepository.Update(parts);
            }
            
            UserVinPartsRequest userrequest = _userVinPartsRequestRepository.GetActiveUserRequest(request.UserId);
            userrequest.IsCompleted = true;
            _userVinPartsRequestRepository.Update(userrequest);
            return Redirect("/Home/Index");
        }

        [Route("GetPartsList")]
        public JsonResult GetPartsList() 
        {
            CompleteActivePartRequestModel response = new CompleteActivePartRequestModel();
            if (Session["UserId"] == null)
            {
                response.Message = "Для подбора запчастей для Вашего автомобиля необходимо авторизоваться";
                response.Arts = new Models.CarModels.PartModel[0];
            }
            else
            {
                int userId = (int)Session["UserId"];
                UsersPartActs model = _usersPartActRepository.GetByUserId(userId);
                if (model != null)
                {
                    response.UserId = userId;
                    string[] parts = model.Arts.Split('|');
                    response.Arts = new Models.CarModels.PartModel[parts.Length];
                    for (int i = 0; i < parts.Length; i++)
                    {
                        int pos = parts[i].IndexOf('-');
                        response.Arts[i] = new Models.CarModels.PartModel()
                        {
                            Title = parts[i].Substring(0, pos),
                            Value = parts[i].Substring(pos + 1, parts[i].Length - pos - 1)
                        };
                    }
                    response.Message = "Мы подобрали для Вас " + response.Arts.Length + " запчастей. Для поиска начните набирать наименование в поле ввода";
                }
                else 
                {
                    response.Message = "Оформите заявку, в течении часа мы ответим о наличие и стоимости";
                    response.Arts = new Models.CarModels.PartModel[0];
                }
            }
            return Json(response);
        }

        [Route("GetShopItems")]
        public JsonResult GetShopItems() 
        {
            string path = Server.MapPath("~/ShopPrice/ShopItems.txt");
            GetCurrentShopItemsResponseModel response = new GetCurrentShopItemsResponseModel();

            using (FileStream fstream = System.IO.File.OpenRead(path))
            {
                byte[] array = new byte[fstream.Length];
                fstream.Read(array, 0, array.Length);
                string ShopItemsAsString = System.Text.Encoding.Default.GetString(array);

                response.Items = ShopItemsAsString.Split('/').ToList();
            }
            return Json(response);
        }

        [HttpGet]
        public ActionResult UploadPrice() 
        {
            return View();
        }

        [HttpPost]
        public string UploadPrice(HttpPostedFileBase upload)
        {
            bool IsCompleted = true;
            string DevMessage = "";
            if (upload != null)
            {
                string UploadStatus = "";
                string path = Server.MapPath("~/ExcelFiles/price.xls");
                try
                {
                    FileInfo file = new FileInfo(path);
                    if (file.Exists) file.Delete();
                    using (FileStream fileToSave = new FileStream(path, FileMode.Create))
                    {
                        upload.InputStream.CopyTo(fileToSave);
                    }
                }
                catch (Exception er)
                {
                    IsCompleted = false;
                    DevMessage += $"Нет доступа к сохранению \n{er.Message} \n{path}";
                }
                try
                {
                    ExcelHandler excel = new ExcelHandler();
                    UploadStatus = excel.LoadPrice(path);
                }
                catch (Exception er) 
                {
                    IsCompleted = false;
                    DevMessage += $"Не удалось обновить списки \n{er.Message} \n{path} \n{UploadStatus}";
                }

            }
            if (IsCompleted)
                return "Успешно обновлено";
            else 
                return DevMessage;
        }
        [HttpGet]
        public ActionResult LoadPartListsTxt() 
        {
            return View();
        }
        [HttpPost]
        public string LoadPartListTxt(HttpPostedFileBase upload) 
        {
            string path = Server.MapPath("~/ShopPrice/ShopItems.txt");
            try
            {
                FileInfo file = new FileInfo(path);
                if (file.Exists) file.Delete();
                using (FileStream fileToSave = new FileStream(path, FileMode.Create))
                {
                    upload.InputStream.CopyTo(fileToSave);
                }
            }
            catch (Exception er) 
            {
                return er.Message;
            }
            return "Успешно обновлено";
        }
    }
}