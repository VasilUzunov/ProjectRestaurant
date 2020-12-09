namespace ProjectRestaurant.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class AllReservationController : AdministrationController
    {
        public IActionResult AllReservations()
        {
            return this.View();
        }
    }
}
