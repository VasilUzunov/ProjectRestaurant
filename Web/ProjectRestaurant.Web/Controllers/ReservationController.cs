namespace ProjectRestaurant.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ProjectRestaurant.Data.Models;
    using ProjectRestaurant.Services.Data;
    using ProjectRestaurant.Services.Messaging;
    using ProjectRestaurant.Web.ViewModels.Reservation;
    using ProjectRestaurant.Web.Areas.Identity.Pages.Account.Manage;

    public class ReservationController : BaseController
    {
        private readonly IReservationsService reservationsService;
        private readonly ISmsSender smsSender;

        public ReservationController(IReservationsService reservationsService, ISmsSender smsSender)
        {
            this.reservationsService = reservationsService;
            this.smsSender = smsSender;
        }

        [Authorize]
        public IActionResult Reservation()
        {
            return this.View();
        }

        [Authorize]
        public IActionResult ShowAll()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var all = this.reservationsService.GetAll<ReservationViewModel>(userId);
            return this.View(all);
        }

        [HttpPost]
        public async Task<IActionResult> Reservation(ReservationInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await this.reservationsService.CreateAsyncReservation(input, userId);

            var phone = this.reservationsService.GetPhoneNumber(userId);

            string trimmedPhone = string.Concat(phone.Where(c => !char.IsWhiteSpace(c)));

            var message = new StringBuilder();

            message.AppendLine("Thank for your reservation")
                .AppendLine($"For: {input.Date} at {input.Hour}:{input.Minutes}")
                .AppendLine($"Your table is №{input.Table} for {input.NumberOfPeople} people");

            var sms = this.smsSender.SendSmsAsync(trimmedPhone, message.ToString().TrimEnd());

            return this.View();
        }
    }
}
