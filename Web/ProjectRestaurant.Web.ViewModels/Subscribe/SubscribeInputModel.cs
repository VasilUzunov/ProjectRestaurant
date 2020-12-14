namespace ProjectRestaurant.Web.ViewModels.Subscribe
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SubscribeInputModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your email")]
        [EmailAddress(ErrorMessage = "Please enter valid email address")]
        public string Email { get; set; }
    }
}
