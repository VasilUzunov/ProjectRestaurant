namespace ProjectRestaurant.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using ProjectRestaurant.Data.Common.Models;

    public class Reservation : BaseDeletableModel<int>
    {
        public int TableId { get; set; }

        public virtual Table Table { get; set; }

        [Required]
        [MaxLength(200)]
        public string Message { get; set; }

        [Required]
        public int NumberOfPeople { get; set; }

        public DateTime DateAndTimeOfReservation { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
