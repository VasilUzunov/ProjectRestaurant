namespace ProjectRestaurant.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using ProjectRestaurant.Data.Common.Models;

    public class Event : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        [MaxLength(1500)]
        public string Description { get; set; }

        public virtual EventImage EventImage { get; set; }
    }
}
