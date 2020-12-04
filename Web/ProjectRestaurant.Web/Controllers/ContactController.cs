namespace ProjectRestaurant.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Contact(ContactFormViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.emailSender.SendEmailAsync("vasil6062@abv.bg", model.Email, "vasil6062@gmail.com", model.Subject + " " + "Name" + " " + model.Name,model.Message);

            return this.Redirect("/Contact/Contact");
        }
    }
}
