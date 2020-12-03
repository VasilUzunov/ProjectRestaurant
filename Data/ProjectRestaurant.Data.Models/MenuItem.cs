namespace ProjectRestaurant.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ProjectRestaurant.Data.Common.Models;

    public class MenuItem : BaseDeletableModel<int>
    {
        public MenuItem()
        {
            this.Reservations = new HashSet<ReservationMenu>();
            this.FavoriteItems = new HashSet<FavoriteMenuItem>();
        }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        [Range(0, 800)]
        public double PortionWeight { get; set; }

        [Required]
        [Range(0, 40)]
        public double Price { get; set; }

        [Required]
        [MaxLength(1500)]
        public string Description { get; set; }

        public MenuImage MenuImage { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<ReservationMenu> Reservations { get; set; }

        public virtual ICollection<FavoriteMenuItem> FavoriteItems { get; set; }
    }
}
