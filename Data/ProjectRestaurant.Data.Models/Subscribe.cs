namespace ProjectRestaurant.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using ProjectRestaurant.Data.Common.Models;

    public class Subscribe : BaseModel<int>
    {
        [Required]
        public string Email { get; set; }
    }
}
