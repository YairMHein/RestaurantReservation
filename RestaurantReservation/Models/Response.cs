using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantReservation.Models
{
    public class ResponseViewModel
    {
        public int MessageType {  get; set; }
        public string Message {  get; set; }
    }
}