using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace RestaurantReservation.Data.Models
{
    [Table("tbRestaurant")]
    public class tbRestaurant
    {
        [Key]
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
    }
}