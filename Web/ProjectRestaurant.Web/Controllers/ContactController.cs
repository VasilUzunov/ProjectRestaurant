namespace ProjectRestaurant.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ContactController : BaseController
    {
        public IActionResult Contact()
        {
            return this.View();
        }
    }
}
