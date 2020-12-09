namespace ProjectRestaurant.Web.ViewModels.Administration
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class EventInputModel
    {
        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        [MinLength(100)]
        [MaxLength(1500)]
        public string Description { get; set; }

        public IFormFile Image { get; set; }
    }
}
