namespace ProjectRestaurant.Web.ViewModels.NewFolder
{
    using System.Collections.Generic;

    using ProjectRestaurant.Web.ViewModels.Event;
    using ProjectRestaurant.Web.ViewModels.Vote;

    public class HomeViewModel
    {
        public IEnumerable<VoteViewModel> Votes { get; set; }

        public IEnumerable<EventViewModel> Events { get; set; }
    }
}
