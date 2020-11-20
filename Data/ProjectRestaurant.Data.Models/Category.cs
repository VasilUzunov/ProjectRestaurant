namespace ProjectRestaurant.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ProjectRestaurant.Data.Common.Models;

    public class Category : BaseModel<int>
    {
        public Category()
        {
            this.MenuItem = new HashSet<MenuItem>();
        }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public virtual ICollection<MenuItem> MenuItem { get; set; }
    }
}
