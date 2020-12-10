namespace ProjectRestaurant.Web.ViewModels.Vote
{
    using System.ComponentModel.DataAnnotations;

    public class VoteInputModel
    {
        [Required]
        [MinLength(5)]
        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        [Range(1, 5)]
        public int Star { get; set; }
    }
}
