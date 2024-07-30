using RestaurantReservation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Service.IRepository
{
    public interface IUser
    {
        UserViewModel Authenticate(string username, string password);
        ResponseViewModel Register(UserViewModel model);
        ResponseViewModel Reserve(ReservationViewModel model);
        List<ReservationViewModel> GetReservationListByUserId(int userid);
        List<RestaurantViewModel> GetRestaurantList();

    }
}
