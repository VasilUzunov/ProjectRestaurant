namespace ProjectRestaurant.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using ProjectRestaurant.Data.Common.Models;

    public class Rating : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(1, 5)]
        public int Star { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
