using RestaurantReservation.Data.Models;
using RestaurantReservation.Models;
using RestaurantReservation.Service;
using RestaurantReservation.Service.IRepository;
using RestaurantReservation.Service.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace RestaurantReservation.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdmin _iadmin;
        private readonly IRestaurant _irestaurant;
        private readonly IUser _iuser;
        public AdminController(IAdmin admin, IRestaurant irestaurant, IUser iuser)
        {
            _iadmin = admin;
            _irestaurant = irestaurant;
            _iuser = iuser;
        }
        // GET: Admin
        #region Login
        // GET: Login
        public ActionResult Login()
        {
            AdminViewModel model = new AdminViewModel();
            HttpCookie reqCookies = Request.Cookies["LoopAdminSystemInfo"];
            if (reqCookies != null)
            {
                Response.Cookies["LoopAdminSystemInfo"].Expires = Common.getLocalTime(DateTime.UtcNow).Date.AddDays(-1);
            }
            Common.ClearLoginData();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(AdminViewModel model)
        {
            model = _iadmin.Authenticate(model.UserName, model.Password);
            if (model.MessageType == 1)
            {
                Common.StoreLoginData(model);
            }
            return Json(model);
        }
        #endregion

        #region Index
        public ActionResult Index()
        {
            HttpCookie reqCookies = Request.Cookies["LoopAdminSystemInfo"];
            if (reqCookies != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }


        }
        #endregion

        #region Users
        public ActionResult Users()
        {
            HttpCookie reqCookies = Request.Cookies["LoopAdminSystemInfo"];
            if (reqCookies != null)
            {
                UserViewModel model = new UserViewModel();
                return View(model);
            }
            else
            {
                return RedirectToAction("Admin", "Login");
            }
        }

        public ActionResult GetListUsers()
        {
            List<UserViewModel> list = _iadmin.GetUserList();
            return Json(list);
        }
        #endregion

        #region Restaurant
        public ActionResult Restaurant()
        {
            HttpCookie reqCookies = Request.Cookies["LoopAdminSystemInfo"];
            if (reqCookies != null)
            {
                RestaurantViewModel model = new RestaurantViewModel();
                return View(model);
            }
            else
            {
                return RedirectToAction("Admin", "Login");
            }


        }

        public ActionResult GetListRestaurant()
        {
            List<RestaurantViewModel> list = _irestaurant.GetList();
            return Json(list);
        }

        public ActionResult GetByIDRestaurant(int ID)
        {
            HttpCookie reqCookies = Request.Cookies["LoopAdminSystemInfo"];
            if (reqCookies != null)
            {
                RestaurantViewModel model = _irestaurant.GetByID(ID);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        [HttpPost]
        public ActionResult UpsertRestaurant(RestaurantViewModel model)
        {
            ResponseViewModel result = new ResponseViewModel();
            HttpCookie reqCookies = Request.Cookies["LoopAdminSystemInfo"];
            if (reqCookies != null)
            {
                if (model.Id != 0)
                {
                    result = _irestaurant.Update(model);
                }
                else
                {
                    result = _irestaurant.Save(model);
                }
            }
            else
            {
                result.MessageType = Settings.LogoutCode;
                result.Message = Settings.SessionTimeout;
            }
            return Json(result);
        }


        public ActionResult DeleteRestaurant(int ID)
        {
            ResponseViewModel result = new ResponseViewModel();
            HttpCookie reqCookies = Request.Cookies["LoopAdminSystemInfo"];
            if (reqCookies != null)
            {
                result = _irestaurant.Delete(ID);
            }
            else
            {
                result.MessageType = Settings.LogoutCode;
                result.Message = Settings.SessionTimeout;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Reservation
        public ActionResult Reservation()
        {
            HttpCookie reqCookies = Request.Cookies["LoopAdminSystemInfo"];
            if (reqCookies != null)
            {
                ReservationViewModel model = new ReservationViewModel();
                return View(model);
            }
            else
            {
                return RedirectToAction("Admin", "Login");
            }
        }

        public ActionResult GetListReservation()
        {
            List<ReservationViewModel> list = _iadmin.GetReservationList();
            return Json(list);
        }

        #endregion
    }
}