using RestaurantReservation.Data.Models;
using RestaurantReservation.Models;
using RestaurantReservation.Service;
using RestaurantReservation.Service.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantReservation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUser _iuser;
        private readonly IRestaurant _irestaurant;
        public HomeController(IUser iuser, IRestaurant irestaurant)
        {
            _iuser = iuser;
            _irestaurant = irestaurant;
        }

        public ActionResult Index()
        {
            RestaurantViewModel model = new RestaurantViewModel();
            model.lstRestaurant = _iuser.GetRestaurantList();
            return View(model);
        }

        #region Login/SignUp
        public ActionResult Login()
        {
            UserViewModel model = new UserViewModel();
            HttpCookie reqCookies = Request.Cookies["LoopClientSystemInfo"];
            if (reqCookies != null)
            {
                Response.Cookies["LoopClientSystemInfo"].Expires = Common.getLocalTime(DateTime.UtcNow).Date.AddDays(-1);
            }
            Common.ClientClearLoginData();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(UserViewModel model)
        {
            model = _iuser.Authenticate(model.UserName, model.Password);
            if (model.MessageType == 1)
            {
                Common.ClientStoreLoginData(model);
            }
            return Json(model);
        }

        public ActionResult Logout()
        {
            Common.ClientClearLoginData();
            return RedirectToAction("Index");
        }

        public ActionResult Signup()
        {
            UserViewModel model = new UserViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(UserViewModel model)
        {
            ResponseViewModel result = _iuser.Register(model);
            return Json(result);
        }
        #endregion

        #region Reserve
        public ActionResult Reserve(int restaurant)
        {
            HttpCookie reqCookies = Request.Cookies["LoopClientSystemInfo"];
            if (reqCookies != null)
            {
                ReservationViewModel reservation = new ReservationViewModel();
                reservation.RestaurantId = restaurant;
                reservation.UserId = int.Parse(reqCookies["ID"].ToString());
                reservation.restaurantModel = _irestaurant.GetByID(restaurant);
                return View(reservation);
            }
            else{
                return RedirectToAction("Login");
            }
          
        }

        public ActionResult MakeReserve(ReservationViewModel model)
        {
            HttpCookie reqCookies = Request.Cookies["LoopClientSystemInfo"];
            if (reqCookies != null)
            {
                //Response.Cookies["LoopClientSystemInfo"].Expires = Common.getLocalTime(DateTime.UtcNow).Date.AddDays(-1);
                model.UserId = int.Parse(reqCookies["ID"].ToString());
                ResponseViewModel response = _iuser.Reserve(model);
                return Json(response);
            }
            else
            {
                return RedirectToAction("Login");
            }

        }

        #endregion

        public ActionResult BookingList()
        {
            HttpCookie reqCookies = Request.Cookies["LoopClientSystemInfo"];
            if (reqCookies != null)
            {
                int userId = int.Parse(reqCookies["ID"].ToString());

                ReservationViewModel model = new ReservationViewModel();
                model.lstBookings = _iuser.GetReservationListByUserId(userId);
                return View(model);
            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }
    }
}