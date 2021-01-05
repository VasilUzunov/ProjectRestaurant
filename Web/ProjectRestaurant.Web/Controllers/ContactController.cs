namespace ProjectRestaurant.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using ProjectRestaurant.Common;
    using ProjectRestaurant.Services.Messaging;
    using ProjectRestaurant.Web.ViewModels.Contact;

    public class ContactController : BaseController
    {
        private readonly IEmailSender emailSender;

        public ContactController(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        public IActionResult Contact()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactFormInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.emailSender.SendEmailAsync("vasil6062@abv.bg", input.Email, "vasil6062@gmail.com", input.Subject + " " + "Name" + " " + input.Name, input.Message);

            return this.View();
        }
    }
}
