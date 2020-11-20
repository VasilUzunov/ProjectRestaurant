namespace ProjectRestaurant.Data.Models
{
    using System.Collections.Generic;

    using ProjectRestaurant.Data.Common.Models;

    public class Favorite : BaseDeletableModel<int>
    {
        public Favorite()
        {
            this.FavoriteItems = new HashSet<FavoriteMenuItem>();
        }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<FavoriteMenuItem> FavoriteItems { get; set; }
    }
}
