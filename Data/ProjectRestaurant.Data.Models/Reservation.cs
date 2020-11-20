namespace ProjectRestaurant.Data.Models
{
    using System;
    using System.Collections.Generic;

    using ProjectRestaurant.Data.Common.Models;

    public class Reservation : BaseDeletableModel<int>
    {
        public Reservation()
        {
            this.MenuItems = new HashSet<ReservationMenu>();
        }

        public int TableId { get; set; }

        public virtual Table Table { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public DateTime DateAndTimeOfReservation { get; set; }

        public virtual ICollection<ReservationMenu> MenuItems { get; set; }
    }
}
