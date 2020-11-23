namespace ProjectRestaurant.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ReservationController : BaseController
    {
        [Authorize]
        public IActionResult MakeReservation()
        {
            return this.View();
        }
    }
}
