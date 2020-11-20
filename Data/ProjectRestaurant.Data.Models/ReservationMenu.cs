namespace ProjectRestaurant.Data.Models
{
    using ProjectRestaurant.Data.Common.Models;

    public class ReservationMenu
    {
        public int Id { get; set; }

        public int ReservationId { get; set; }

        public virtual Reservation Reservation { get; set; }

        public int MenuId { get; set; }

        public virtual MenuItem MenuItem { get; set; }
    }
}
