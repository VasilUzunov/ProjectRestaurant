namespace ProjectRestaurant.Web.ViewModels.Subscribe
{
    using System.ComponentModel.DataAnnotations;

    using ProjectRestaurant.Web.Infrastructure;

    public class SubscribeInputModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your email")]
        [EmailAddress(ErrorMessage = "Please enter valid email address")]
        public string Email { get; set; }

        [GoogleReCaptchaValidation]
        public string RecaptchaValue { get; set; }
    }
}
