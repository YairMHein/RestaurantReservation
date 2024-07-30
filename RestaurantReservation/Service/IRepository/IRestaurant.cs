using RestaurantReservation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Service.IRepository
{
    public interface IRestaurant
    {
        List<RestaurantViewModel> GetList();
        RestaurantViewModel GetByID(int Id);
        ResponseViewModel Save(RestaurantViewModel model);
        ResponseViewModel Update(RestaurantViewModel model);
        ResponseViewModel Delete(int Id);
    }
}
