namespace ProjectRestaurant.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ReservationController : BaseController
    {
        public IActionResult MakeReservation()
        {
            return this.View();
        }
    }
}
