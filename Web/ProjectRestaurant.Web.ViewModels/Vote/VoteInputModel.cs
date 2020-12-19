namespace ProjectRestaurant.Web.ViewModels.Vote
{
    using System.ComponentModel.DataAnnotations;

    public class VoteInputModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please write review")]
        [StringLength(1000, ErrorMessage = "Review must be at least {2} and not more than {1} symbols.", MinimumLength = 5)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter star")]
        [Range(1, 5, ErrorMessage = "Stars must be in range from {1} to {2}.")]
        public int Star { get; set; }
    }
}
