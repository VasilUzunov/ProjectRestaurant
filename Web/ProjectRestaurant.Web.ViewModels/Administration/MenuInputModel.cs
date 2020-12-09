namespace ProjectRestaurant.Web.ViewModels.Administration
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class MenuInputModel
    {
        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public double PortionWeight { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string Description { get; set; }

        public IFormFile Image { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string CategoryName { get; set; }
    }
}
