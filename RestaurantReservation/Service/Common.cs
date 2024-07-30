using RestaurantReservation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantReservation.Service
{
    public static class Common
    {
        #region ClearLoginData
        public static void ClearLoginData()
        {
            if (HttpContext.Current.Request.Cookies["LoopAdminSystemInfo"] != null)
            {
                HttpCookie httpCookie = HttpContext.Current.Request.Cookies["LoopAdminSystemInfo"];

                httpCookie.Value = null;
                httpCookie.Expires = getLocalTime(DateTime.UtcNow).Date.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(httpCookie);
            }
        }

        #endregion

        #region StoreLoginData
        public static void StoreLoginData(AdminViewModel model)
        {

            HttpCookie tcinfo = new HttpCookie("LoopAdminSystemInfo");
            tcinfo["ID"] = model.Id.ToString();
            tcinfo["username"] = model.UserName;
            tcinfo["fullname"] = model.FullName;
            

            tcinfo.Expires.Add(new TimeSpan(1));
            HttpContext.Current.Response.Cookies.Add(tcinfo);
        }


        #endregion

        #region GetLoginData
        public static AdminViewModel GetLoginData()
        {
            AdminViewModel model = new AdminViewModel();
            HttpCookie reqCookies = HttpContext.Current.Request.Cookies["LoopAdminSystemInfo"];
            if (reqCookies != null)
            {
                model.Id = int.Parse(reqCookies["ID"].ToString());
                model.UserName = reqCookies["username"].ToString();
                model.FullName = reqCookies["fullname"].ToString();
            }
            return model;
        }

        #endregion

        public static DateTime getLocalTime(this DateTime utc)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(utc, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
        }

        public static string ChangeFormatYearMonthDay(string date)
        {
            string rtnDate = "";
            try
            {
                string[] str = date.Split('/');
                if (str.Length > 0)
                {
                    rtnDate = (str[2] + "-" + str[1] + "-" + str[0]).ToString();
                }
            }
            catch { }
            return rtnDate;
        }

        #region ClientClearLoginData
        public static void ClientClearLoginData()
        {
            if (HttpContext.Current.Request.Cookies["LoopClientSystemInfo"] != null)
            {
                HttpCookie httpCookie = HttpContext.Current.Request.Cookies["LoopClientSystemInfo"];

                httpCookie.Value = null;
                httpCookie.Expires = getLocalTime(DateTime.UtcNow).Date.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(httpCookie);
            }
        }

        #endregion

        #region ClientStoreLoginData
        public static void ClientStoreLoginData(UserViewModel model)
        {

            HttpCookie cinfo = new HttpCookie("LoopClientSystemInfo");
            cinfo["ID"] = model.Id.ToString();
            cinfo["username"] = model.UserName;
            cinfo["fullname"] = model.FullName;


            cinfo.Expires.Add(new TimeSpan(1));
            HttpContext.Current.Response.Cookies.Add(cinfo);
        }


        #endregion

        #region ClientGetLoginData
        public static UserViewModel ClientGetLoginData()
        {
            UserViewModel model = new UserViewModel();
            HttpCookie reqCookies = HttpContext.Current.Request.Cookies["LoopClientSystemInfo"];
            if (reqCookies != null)
            {
                model.Id = int.Parse(reqCookies["ID"].ToString());
                model.UserName = reqCookies["username"].ToString();
                model.FullName = reqCookies["fullname"].ToString();
            }
            return model;
        }

        #endregion

    }
}