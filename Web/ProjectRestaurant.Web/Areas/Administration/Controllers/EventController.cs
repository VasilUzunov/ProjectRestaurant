using ProjectRestaurant.Web.ViewModels.Event;

namespace ProjectRestaurant.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using ProjectRestaurant.Services.Data;
    using ProjectRestaurant.Web.ViewModels.Administration;

    public class EventController : AdministrationController
    {
        private readonly IEventService eventService;
        private readonly IWebHostEnvironment environment;

        public EventController(IEventService eventService, IWebHostEnvironment environment)
        {
            this.eventService = eventService;
            this.environment = environment;
        }

        public IActionResult AddEvents()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEvents(EventInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.eventService.CreateAsyncEvent(input, $"{this.environment.WebRootPath}/images");

            return this.Redirect("/Administration/Event/AddEvents");
        }

        public IActionResult AllEvents()
        {
            var model = this.eventService.GetAll<EventViewModel>();
            return this.View(model);
        }
    }
}
