namespace ProjectRestaurant.Web.ViewModels.Menu
{
    using AutoMapper;
    using ProjectRestaurant.Data.Models;
    using ProjectRestaurant.Services.Mapping;

    public class MenuViewModel : IMapFrom<MenuItem>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public double PortionWeight { get; set; }

        public string ImageUrl { get; set; }

        public string CategoryName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<MenuItem, MenuViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        x.MenuImage.RemoteImageUrl ?? "/images/menu/" + x.MenuImage.Id + "." + x.MenuImage.Extension));
        }
    }
}
