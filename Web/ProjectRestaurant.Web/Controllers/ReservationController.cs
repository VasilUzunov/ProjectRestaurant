namespace ProjectRestaurant.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ProjectRestaurant.Web.ViewModels.Reservation;

    public class ReservationController : BaseController
    {
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

            return this.View();
        }
    }
}
