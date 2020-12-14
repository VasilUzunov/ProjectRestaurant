namespace ProjectRestaurant.Web.Controllers
{
    using System.Diagnostics;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using ProjectRestaurant.Services.Data;
    using ProjectRestaurant.Services.Messaging;
    using ProjectRestaurant.Web.ViewModels;
    using ProjectRestaurant.Web.ViewModels.Event;
    using ProjectRestaurant.Web.ViewModels.NewFolder;
    using ProjectRestaurant.Web.ViewModels.Subscribe;
    using ProjectRestaurant.Web.ViewModels.Vote;

    public class HomeController : BaseController
    {
        private readonly IEmailSender emailSender;
        private readonly IVoteService voteService;
        private readonly ISubscribeService subscribeService;
        private readonly IEventService eventService;

        public HomeController(IEventService eventService, IEmailSender emailSender, IVoteService voteService, ISubscribeService subscribeService)
        {
            this.emailSender = emailSender;
            this.voteService = voteService;
            this.subscribeService = subscribeService;
            this.eventService = eventService;
        }

        public IActionResult Index()
        {
            var eventView = this.eventService.GetAll<EventViewModel>();
            var voteView = this.voteService.GetAll<VoteViewModel>();
            var homeView = new HomeViewModel { Events = eventView, Votes = voteView };
            return this.View(homeView);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> SendToEmail(SubscribeInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                if (input == null)
                {
                    this.SetFlash("You must write your email!");
                }

                return this.RedirectToAction("Index");
            }

            await this.subscribeService.AddAsyncSubscriber(input.Email);
            var html = new StringBuilder();
            html.AppendLine($"<h1>You are subscribed for our newsletter!</h1>");
            await this.emailSender.SendEmailAsync("vasil6062@abv.bg", "Pause Restaurant", input.Email, "Subscribe", html.ToString());
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
