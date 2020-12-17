namespace ProjectRestaurant.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ProjectRestaurant.Services.Data;
    using ProjectRestaurant.Web.ViewModels.Reservation;

    public class AllReservationController : AdministrationController
    {
        private readonly IReservationsService reservationsService;

        public AllReservationController(IReservationsService reservationsService)
        {
            this.reservationsService = reservationsService;
        }

        public IActionResult AllReservations()
        {
            var model = this.reservationsService.GetAll<ReservationViewModel>();
            return this.View(model);
        }
    }
}
