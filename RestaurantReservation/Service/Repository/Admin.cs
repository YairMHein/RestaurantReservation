using RestaurantReservation.Data.Models;
using RestaurantReservation.Models;
using RestaurantReservation.Service.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantReservation.Service.Repository
{
    public class Admin:IAdmin
    {
        private readonly LoopDBContext _context;
        public Admin(LoopDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public AdminViewModel Authenticate(string username, string password)
        {
            AdminViewModel model = new AdminViewModel();
            try
            {
                var query = (from data in _context.tbAdmins
                             where data.UserName == username && data.Password == password && data.IsDeleted == false
                             select new AdminViewModel
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
                        model = query.AsEnumerable().Select((data, index) => new AdminViewModel()
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

        public List<ReservationViewModel> GetReservationList()
        {
            List<ReservationViewModel> lst = new List<ReservationViewModel>();
            try
            {
                var query = (from data in _context.tbReservations
                             join restaurant in _context.tbRestaurants on data.RestaurantId equals restaurant.Id
                             join users in _context.tbUsers on data.UserId equals users.Id
                             select new ReservationViewModel
                             {
                                 Id = data.Id,
                                 RestaurantId = data.RestaurantId,
                                 ReservationDate = data.ReservationDate,
                                 ReservationTime = data.ReservationTime,
                                 NumberPeople = data.NumberPeople,
                                 Remark = data.Remark,
                                 RestaurantName = restaurant.Name,
                                 CustomerName = users.FullName,
                             }).OrderByDescending(x => x.ReservationTime);
                lst = query.AsEnumerable().Select((data, index) => new ReservationViewModel()
                {
                    Id = data.Id,
                    RestaurantId = data.RestaurantId,
                    ReservationDate = data.ReservationDate,
                    ReservationTime = data.ReservationTime,
                    sReservationDate = data.ReservationTime.ToString("dd/MM/yyyy ddd"),
                    sReservationTime = data.ReservationTime.ToString("hh:mm tt"),
                    NumberPeople = data.NumberPeople,
                    Remark = data.Remark,
                    RestaurantName = data.RestaurantName,
                    CustomerName = data.CustomerName,
                    No = ++index
                }).ToList();
            }
            catch
            {
                lst = new List<ReservationViewModel>();
            }
            return lst;
        }

        public List<UserViewModel> GetUserList()
        {
            List<UserViewModel> lst = new List<UserViewModel>();
            try
            {
                var query = (from data in _context.tbUsers
                             select new UserViewModel
                             {
                                 Id = data.Id,
                                 UserName = data.UserName,
                                 FullName = data.FullName,
                                 Phone = data.Phone,
                                 CreateDate = data.CreateDate,
                             }).OrderByDescending(x => x.CreateDate);
                lst = query.AsEnumerable().Select((data, index) => new UserViewModel()
                {
                    Id = data.Id,
                    UserName = data.UserName,
                    FullName = data.FullName,
                    Phone = data.Phone,
                    sCreateDate = data.CreateDate.ToString("dd/MM/yyyy"),
                    No = ++index
                }).ToList();
            }
            catch
            {
                lst = new List<UserViewModel>();
            }
            return lst;
        }
    }
}