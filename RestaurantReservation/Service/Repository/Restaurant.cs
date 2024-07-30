using RestaurantReservation.Data.Models;
using RestaurantReservation.Models;
using RestaurantReservation.Service.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Web;

namespace RestaurantReservation.Service.Repository
{
    public class Restaurant:IRestaurant
    {
        LoopDBContext _context;
        public Restaurant(LoopDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public List<RestaurantViewModel> GetList()
        {
            List<RestaurantViewModel> lst = new List<RestaurantViewModel>();
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
                                 IsDeleted = data.IsDeleted,
                                 CreateDate = data.CreateDate,
                             });
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
                    IsDeleted = data.IsDeleted,
                    CreateDate = data.CreateDate,
                    No = ++index
                }).ToList();
            }
            catch
            {
                lst = new List<RestaurantViewModel>();
            }
            return lst;
        }

        public RestaurantViewModel GetByID(int id)
        {
            RestaurantViewModel model = new RestaurantViewModel();
            try
            {
                var query = (from data in _context.tbRestaurants
                             where data.IsDeleted == false && data.Id == id
                             select new RestaurantViewModel
                             {
                                 Id = data.Id,
                                 Name = data.Name,
                                 Type = data.Type,
                                 Description = data.Description,
                                 OpenDays = data.OpenDays,
                                 FromTimeAMPM = data.FromTimeAMPM,
                                 FromTimeClock = data.FromTimeClock,
                                 ToTimeAMPM = data.ToTimeAMPM,
                                 ToTimeClock = data.ToTimeClock,
                                 IsDeleted = data.IsDeleted,
                                 CreateDate = data.CreateDate,
                             });
                model = query.AsEnumerable().Select((data, index) => new RestaurantViewModel()
                {
                    Id = data.Id,
                    Name = data.Name,
                    Type = data.Type,
                    Description = data.Description,
                    OpenDays = data.OpenDays,
                    FromTimeAMPM = data.FromTimeAMPM,
                    FromTimeClock = data.FromTimeClock,
                    ToTimeAMPM = data.ToTimeAMPM,
                    ToTimeClock = data.ToTimeClock,
                    IsDeleted = data.IsDeleted,
                    CreateDate = data.CreateDate,
                }).First();
            }
            catch
            {
                model = new RestaurantViewModel();
            }
            return model;
        }

        public ResponseViewModel Save(RestaurantViewModel model)
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {
                tbRestaurant obj = new tbRestaurant();
                obj.Name = model.Name;
                obj.Type = model.Type;
                obj.Description = model.Description;
                obj.OpenDays = model.OpenDays;
                obj.FromTimeClock = model.FromTimeClock;
                obj.FromTimeAMPM = model.FromTimeAMPM;
                obj.ToTimeClock = model.ToTimeClock;
                obj.ToTimeAMPM = model.ToTimeAMPM;
                obj.IsDeleted = false;
                obj.CreateDate = Common.getLocalTime(DateTime.UtcNow);
                _context.tbRestaurants.Add(obj);
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

        public ResponseViewModel Update(RestaurantViewModel model)
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {
                _context.tbRestaurants.First(x => x.Id == model.Id).Name = model.Name;

                int res = _context.SaveChanges();
                if (res == 1)
                {
                    response.MessageType = Settings.SuccessCode;
                    response.Message = Settings.UpdateSuccess;
                }
                else
                {
                    response.MessageType = Settings.FailCode;
                    response.Message = Settings.UpdateFail;
                }
            }
            catch
            {

                response.MessageType = Settings.FailCode;
                response.Message = Settings.ErrorOccur;
            }
            return response;
        }
        public ResponseViewModel Delete(int id)
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {
                _context.tbRestaurants.First(x => x.Id == id).IsDeleted = true;
                int res = _context.SaveChanges();
                if (res == 1)
                {
                    response.MessageType = Settings.SuccessCode;
                    response.Message = Settings.DeleteSuccess;
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
    }
}