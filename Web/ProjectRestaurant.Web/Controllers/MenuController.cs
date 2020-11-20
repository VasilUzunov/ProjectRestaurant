namespace ProjectRestaurant.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class MenuController : BaseController
    {
        public IActionResult Menu()
        {
            return this.View();
        }
    }
}
