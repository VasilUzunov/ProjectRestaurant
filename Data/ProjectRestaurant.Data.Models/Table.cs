namespace ProjectRestaurant.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ProjectRestaurant.Data.Common.Models;

    public class Table : BaseDeletableModel<int>
    {
        public Table()
        {
            this.Reservations = new HashSet<Reservation>();
        }

        [Required]
        [Range(1, 20)]
        public int NumberOfSeats { get; set; }

        [Required]
        [MaxLength(10)]
        public string ShapeOfTable { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
