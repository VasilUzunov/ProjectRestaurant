namespace ProjectRestaurant.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using ProjectRestaurant.Services.Data;
    using ProjectRestaurant.Services.Mapping;
    using ProjectRestaurant.Web.ViewModels;
    using ProjectRestaurant.Web.ViewModels.Administration;

    public class HomeController : BaseController
    {
        private readonly IEventService eventService;

        public HomeController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        public IActionResult Index()
        {
            var eventView = this.eventService.GetAll<EventViewModel>();
            return this.View(eventView);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
