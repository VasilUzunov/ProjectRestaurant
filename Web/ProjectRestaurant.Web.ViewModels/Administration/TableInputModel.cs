namespace ProjectRestaurant.Web.ViewModels.Administration
{
    using System.ComponentModel.DataAnnotations;

    public class TableInputModel
    {
        [Required]
        public int NumberOfSeats { get; set; }

        [Required]
        public string ShapeOfTable { get; set; }

        [Required]
        public int NumberOfTable { get; set; }
    }
}
