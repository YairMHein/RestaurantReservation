using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantReservation.Models
{
    public class AdminViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }


        public int MessageType { get; set; }
        public string Message { get; set; }
    }
}