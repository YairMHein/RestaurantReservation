using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Routing;

namespace RestaurantReservation.Data.Models
{
    public class LoopDBContext : DbContext
    {
        static LoopDBContext()
        {
            Database.SetInitializer<LoopDBContext>(null);
        }
        public LoopDBContext() : base("Name=LoopDBContext")
        {
        }
        public DbSet<tbUser> tbUsers { get; set; }
        public DbSet<tbAdmin> tbAdmins { get; set; }
        public DbSet<tbRestaurant> tbRestaurants { get; set; }
        public DbSet<tbReservation> tbReservations { get; set; }
    }
}
