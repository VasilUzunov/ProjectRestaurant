using ProjectRestaurant.Services.Data;

namespace ProjectRestaurant.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ProjectRestaurant.Data.Models;
    using ProjectRestaurant.Web.ViewModels.Reservation;

    public class ReservationController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IReservationsService reservationsService;

        public ReservationController(UserManager<ApplicationUser> userManager, IReservationsService reservationsService)
        {
            this.userManager = userManager;
            this.reservationsService = reservationsService;
        }

        [Authorize]
        public IActionResult MakeReservation()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> MakeReservation(ReservationInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.reservationsService.CreateAsyncReservation(input, userId);

            return this.Redirect("/Reservation/MakeReservation");
        }
    }
}
