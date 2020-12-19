namespace ProjectRestaurant.Web.ViewModels.Administration
{
    using System.ComponentModel.DataAnnotations;

    public class TableInputModel
    {
        [Required(ErrorMessage = "Please enter number of seats")]
        [Range(1, 20, ErrorMessage = "Number of seats must be in range from {1} to {2}.")]
        public int NumberOfSeats { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter shape")]
        public string ShapeOfTable { get; set; }

        [Required(ErrorMessage = "Please enter table number")]
        public int NumberOfTable { get; set; }
    }
}
