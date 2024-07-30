using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantReservation.Models
{
    public class ReservationViewModel
    {
        public string Id { get; set; }
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime ReservationTime { get; set; }
        public int NumberPeople { get; set; }
        public string Remark { get; set; }
        public DateTime CreateDate { get; set; }
        public string Status { get; set; }
        public string sReservationDate { get; set; }
        public string sReservationTime { get; set; }
        public string CustomerName {  get; set; } 
        public string RestaurantName {  get; set; } 
        public RestaurantViewModel restaurantModel { get; set; }
        public string sTime { get; set; }
        public int No { get; set; }

        public List<ReservationViewModel> lstBookings { get; set;}
    }
}