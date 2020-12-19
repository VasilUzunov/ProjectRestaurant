namespace ProjectRestaurant.Web.ViewModels.Administration
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class EventInputModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter name")]
        [StringLength(50, ErrorMessage = "Name must be at least {2} and not more than {1} symbols.", MinimumLength = 5)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter price")]
        [Range(0, 100000, ErrorMessage = "Price must be in range from {1} to {2}.")]
        public double Price { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter description")]
        [StringLength(1500, ErrorMessage = "Description must be at least {2} and not more than {1} symbols.", MinimumLength = 50)]
        public string Description { get; set; }

        public IFormFile Image { get; set; }
    }
}
