namespace ProjectRestaurant.Web.ViewModels.Event
{
    using AutoMapper;
    using ProjectRestaurant.Data.Models;
    using ProjectRestaurant.Services.Mapping;

    public class EventViewModel : IMapFrom<Event>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Event, EventViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        x.EventImage.RemoteImageUrl ?? "/images/events/" + x.EventImage.Id + "." + x.EventImage.Extension));
        }
    }
}
