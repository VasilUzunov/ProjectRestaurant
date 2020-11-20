namespace ProjectRestaurant.Data.Models
{
    public class FavoriteMenuItem
    {
        public int Id { get; set; }

        public int FavoriteId { get; set; }

        public Favorite Favorite { get; set; }

        public int MenuItemId { get; set; }

        public MenuItem MenuItem { get; set; }
    }
}
