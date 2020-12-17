namespace ProjectRestaurant.Web.ViewModels.Reservation
{
    using System;

    using ProjectRestaurant.Services.Mapping;

    public class ReservationViewModel : IMapFrom<Data.Models.Reservation>
    {
        public int TableTableNumber { get; set; }

        public int NumberOfPeople { get; set; }

        public string Message { get; set; }

        public DateTime DateAndTimeOfReservation { get; set; }

        public string DateAndTimeOfReservationInString => this.DateAndTimeOfReservation.ToString("F");

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public string UserFullName => this.UserFirstName + " " + this.UserLastName;

        public DateTime CreatedOn { get; set; }

        public string CreatedOnInString => this.CreatedOn.ToString("F");
    }
}
