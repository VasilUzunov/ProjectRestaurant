namespace ProjectRestaurant.Web.ViewModels.Reservation
{
    public class ReservationInputModel
    {
        public string Date { get; set; }

        public string Hour { get; set; }

        public string Minutes { get; set; }

        public int Table { get; set; }

        public int NumberOfPeople { get; set; }

        public string Message { get; set; }
    }
}
