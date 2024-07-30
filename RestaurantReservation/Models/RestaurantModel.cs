using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantReservation.Models
{
    public class RestaurantViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string OpenDays { get; set; }
        public int FromTimeClock { get; set; }
        public string FromTimeAMPM { get; set; }
        public int ToTimeClock { get; set; }
        public string ToTimeAMPM { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDeleted { get; set; }
        public string IsOpenToday { get; set; }
        public int No {  get; set; }
        public List<RestaurantViewModel> lstRestaurant {  get; set; }
    }
}