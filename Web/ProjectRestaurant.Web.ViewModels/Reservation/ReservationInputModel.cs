namespace ProjectRestaurant.Web.ViewModels.Reservation
{
    using System.ComponentModel.DataAnnotations;

    public class ReservationInputModel
    {
        [Required(ErrorMessage = "Please enter your name")]
        [DataType("mm/dd/yyyy", ErrorMessage = "Please enter date in type mm/dd/yyy")]
        public string Date { get; set; }

        [Required(ErrorMessage = "Please enter hour")]
        [Range(10, 22, ErrorMessage = "Hour must be in range from {1} to {2}.")]
        public string Hour { get; set; }

        [Required(ErrorMessage = "Please enter minutes")]
        [Range(00, 59, ErrorMessage = "Hour must be in range from {1} to {2}.")]
        public string Minutes { get; set; }

        [Required(ErrorMessage = "Please enter preferred table")]
        [Range(1, 20, ErrorMessage = "Hour must be in range from {1} to {2}.")]
        public int Table { get; set; }

        [Required(ErrorMessage = "Please enter number of people")]
        [Range(1, 20, ErrorMessage = "Hour must be in range from {1} to {2}.")]
        public int NumberOfPeople { get; set; }

        [StringLength(1000, ErrorMessage = "Message must be at least {2} and not more than {1} symbols.", MinimumLength = 5)]
        public string Message { get; set; }
    }
}
