namespace ProjectRestaurant.Web.ViewModels.Vote
{
    using ProjectRestaurant.Data.Models;
    using ProjectRestaurant.Services.Mapping;

    public class VoteViewModel : IMapFrom<Rating>
    {
        public string Description { get; set; }

        public int Star { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }
    }
}
