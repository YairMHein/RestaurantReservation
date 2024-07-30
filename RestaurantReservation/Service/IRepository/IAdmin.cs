using RestaurantReservation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Service.IRepository
{
    public interface IAdmin
    {
        AdminViewModel Authenticate(string username, string password);
        List<ReservationViewModel> GetReservationList();
        List<UserViewModel> GetUserList();
    }
}
