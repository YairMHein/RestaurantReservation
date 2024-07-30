using RestaurantReservation.Data.Models;
using RestaurantReservation.Models;
using RestaurantReservation.Service.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace RestaurantReservation.Service.Repository
{
    public class User:IUser
    {
        LoopDBContext _context;
        public User(LoopDBContext context)
        {
            _context = context;
        }
        public UserViewModel Authenticate(string username, string password)
        {
            UserViewModel model = new UserViewModel();
            try
            {
                var query = (from data in _context.tbUsers
                             where data.UserName == username && data.Password == password && data.IsDeleted == false
                             select new UserViewModel
                             {
                                 Id = data.Id,
                                 UserName = data.UserName,
                                 FullName = data.FullName,
                                 IsActive = data.IsActive,
                             }).ToList();
                if (query.Count > 0)
                {
                    if (query.FirstOrDefault().IsActive == false)
                    {
                        model.MessageType = 2;
                        model.Message = "This user account is currently disabled.";
                    }
                    else
                    {
                        model = query.AsEnumerable().Select((data, index) => new UserViewModel()
                        {
                            Id = data.Id,
                            UserName = data.UserName,
                            FullName = data.FullName,
                        }).FirstOrDefault();
                        model.MessageType = 1;

                    }

                }
                else
                {
                    model.MessageType = 2;
                    model.Message = "User name or Password is incorrect!";
                }
            }
            catch (Exception e)
            {
                model.MessageType = 2;
                model.Message = e.Message;
            }
            return model;
        }

        public ResponseViewModel Register(UserViewModel model)
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {
                tbUser obj = new tbUser();
                obj.UserName = model.UserName;
                obj.FullName = model.FullName;
                obj.Password = model.Password;
                obj.Phone = model.Phone;
                obj.IsActive = true;
                obj.IsDeleted = false;
                obj.CreateDate = Common.getLocalTime(DateTime.UtcNow);
                _context.tbUsers.Add(obj);
                int res = _context.SaveChanges();
                if (res == 1)
                {
                    response.MessageType = Settings.SuccessCode;
                    response.Message = Settings.SaveSuccess;
                }
                else
                {
                    response.MessageType = Settings.FailCode;
                    response.Message = Settings.SaveFail;
                }
            }
            catch
            {
                response.MessageType = Settings.FailCode;
                response.Message = Settings.ErrorOccur;
            }
            return response;
        }

        public ResponseViewModel Reserve(ReservationViewModel model)
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {
                DateTime reserveDate = Convert.ToDateTime(Common.ChangeFormatYearMonthDay(model.sReservationDate));
                DateTime parsedTime = DateTime.ParseExact(model.sTime, "hh:mm tt", null);
                DateTime reserveDateTime = new DateTime(
                    reserveDate.Year,
                    reserveDate.Month,
                    reserveDate.Day,
                    parsedTime.Hour,
                    parsedTime.Minute,
                    parsedTime.Second
                );
                tbReservation obj = new tbReservation();
                obj.Id = Guid.NewGuid().ToString();
                obj.RestaurantId = model.RestaurantId;
                obj.UserId = model.UserId;
                obj.ReservationDate = reserveDate;
                obj.ReservationTime = reserveDateTime;
                obj.NumberPeople = model.NumberPeople;
                obj.Remark = model.Remark;
                obj.CreateDate = Common.getLocalTime(DateTime.UtcNow);
                _context.tbReservations.Add(obj);
                int res = _context.SaveChanges();
                if (res == 1)
                {
                    response.MessageType = Settings.SuccessCode;
                    response.Message = "Reserved Successful";
                }
                else
                {
                    response.MessageType = Settings.FailCode;
                    response.Message = "Reserved failed";
                }
            }
            catch
            {
                response.MessageType = Settings.FailCode;
                response.Message = Settings.ErrorOccur;
            }
            return response;
        }

        public List<ReservationViewModel> GetReservationListByUserId(int userid)
        {
            List<ReservationViewModel> lst = new List<ReservationViewModel>();
            try
            {
                var query = (from data in _context.tbReservations
                             join restaurant in _context.tbRestaurants on data.RestaurantId equals restaurant.Id
                             where data.UserId == userid
                             select new ReservationViewModel
                             {
                                 Id = data.Id,
                                 RestaurantId = data.RestaurantId,
                                 ReservationDate = data.ReservationDate,
                                 ReservationTime = data.ReservationTime,
                                 NumberPeople = data.NumberPeople,
                                 Remark = data.Remark,
                                 RestaurantName = restaurant.Name,
                             }).OrderByDescending(x => x.ReservationTime).ToList();
                lst = query.AsEnumerable().Select((data, index) => new ReservationViewModel()
                {
                    Id = data.Id,
                    RestaurantId = data.RestaurantId,
                    ReservationDate = data.ReservationDate,
                    ReservationTime = data.ReservationTime,
                    sReservationDate = data.ReservationTime.ToString("dd/MM/yyyy"),
                    sReservationTime = data.ReservationTime.ToString("hh:mm tt"),
                    NumberPeople = data.NumberPeople,
                    Remark = data.Remark,
                    RestaurantName = data.RestaurantName,
                }).ToList();
            }
            catch
            {
                lst = new List<ReservationViewModel>();
            }
            return lst;
        }

        public List<RestaurantViewModel> GetRestaurantList()
        {
            List<RestaurantViewModel> lst = new List<RestaurantViewModel>();
            DateTime today = Common.getLocalTime(DateTime.UtcNow);
            string dayofweek = today.DayOfWeek.ToString();
            try
            {
                var query = (from data in _context.tbRestaurants
                             where data.IsDeleted == false
                             select new RestaurantViewModel
                             {
                                 Id = data.Id,
                                 Name = data.Name,
                                 Type = data.Type,
                                 Description = data.Description,
                                 FromTimeAMPM = data.FromTimeAMPM,
                                 FromTimeClock = data.FromTimeClock,
                                 ToTimeAMPM = data.ToTimeAMPM,
                                 ToTimeClock = data.ToTimeClock,
                                 OpenDays = data.OpenDays,
                                 CreateDate = data.CreateDate,
                             }).OrderByDescending(x => x.CreateDate);
                lst = query.AsEnumerable().Select((data, index) => new RestaurantViewModel()
                {
                    Id = data.Id,
                    Name = data.Name,
                    Type = data.Type,
                    Description = data.Description,
                    FromTimeAMPM = data.FromTimeAMPM,
                    FromTimeClock = data.FromTimeClock,
                    ToTimeAMPM = data.ToTimeAMPM,
                    ToTimeClock = data.ToTimeClock,
                    OpenDays = data.OpenDays,
                    IsOpenToday = data.OpenDays.Contains(dayofweek)?"Yes":"No",
                }).ToList();
            }
            catch
            {
                lst = new List<RestaurantViewModel>();
            }
            return lst;
        }
    }
}