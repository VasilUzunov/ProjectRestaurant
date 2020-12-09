namespace ProjectRestaurant.Web.ViewModels.Contact
{
    using System.ComponentModel.DataAnnotations;

    public class ContactFormInputModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your name")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        [EmailAddress(ErrorMessage = "Please enter valid email address")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(100, ErrorMessage = "Subject must be at least {2} and not more than {1} symbols.", MinimumLength = 5)]
        public string Subject { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter message of email")]
        [StringLength(10000, ErrorMessage = "Message must be at least {2} symbols.", MinimumLength = 20)]
        public string Message { get; set; }
    }
}
