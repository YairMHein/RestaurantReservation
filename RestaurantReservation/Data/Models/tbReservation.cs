using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace RestaurantReservation.Data.Models
{
    [Table("tbReservation")]
    public class tbReservation
    {
        [Key]
        public string Id { get; set; }
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime ReservationTime { get; set; }
        public int NumberPeople { get; set; }
        public string Remark { get; set; }
        public DateTime CreateDate { get; set; }
        public string Status { get; set; }
    }
}