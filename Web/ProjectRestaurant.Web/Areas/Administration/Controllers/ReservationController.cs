namespace ProjectRestaurant.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ProjectRestaurant.Services.Data;
    using ProjectRestaurant.Web.ViewModels.Reservation;

    public class ReservationController : AdministrationController
    {
        private readonly IReservationsService reservationsService;

        public ReservationController(IReservationsService reservationsService)
        {
            this.reservationsService = reservationsService;
        }

        public IActionResult Reservations()
        {
            var model = this.reservationsService.GetAll<ReservationViewModel>();
            return this.View(model);
        }
    }
}
