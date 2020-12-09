namespace ProjectRestaurant.Data.Models
{
    using ProjectRestaurant.Data.Common.Models;

    public class Event : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public virtual EventImage EventImage { get; set; }
    }
}
