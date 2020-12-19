namespace ProjectRestaurant.Web.ViewModels.Administration
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class MenuInputModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter name")]
        [StringLength(20, ErrorMessage = "Name must be at least {2} and not more than {1} symbols.", MinimumLength = 4)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter portion weight")]
        [Range(1, 2000, ErrorMessage = "Portion weight must be in range from {1} to {2}.")]
        public double PortionWeight { get; set; }

        [Required(ErrorMessage = "Please enter price")]
        [Range(0, 300, ErrorMessage = "Price must be in range from {1} to {2}.")]
        public double Price { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter description")]
        [StringLength(200, ErrorMessage = "Description must be at least {2} and not more than {1} symbols.", MinimumLength = 5)]
        public string Description { get; set; }

        public IFormFile Image { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter category name")]
        [StringLength(20, ErrorMessage = "Category name must be at least {2} and not more than {1} symbols.", MinimumLength = 5)]
        public string CategoryName { get; set; }
    }
}
