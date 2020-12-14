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

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await this.reservationsService.CreateAsyncReservation(input, userId);

            var phone = this.reservationsService.GetPhoneNumber(userId);

            string trimmedPhone = string.Concat(phone.Where(c => !char.IsWhiteSpace(c)));

            var message = new StringBuilder();

            message.AppendLine("Thank for your reservation")
                .AppendLine($"For: {input.Date} at {input.Hour}:{input.Minutes}")
                .AppendLine($"Your table is №{input.Table} for {input.NumberOfPeople} people");

            var sms = this.smsSender.SendEmailAsync(trimmedPhone, message.ToString().TrimEnd());

            return this.View();
        }
    }
}
