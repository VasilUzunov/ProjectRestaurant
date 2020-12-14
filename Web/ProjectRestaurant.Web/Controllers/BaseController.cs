namespace ProjectRestaurant.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class BaseController : Controller
    {
        public void SetFlash(string text)
        {
            this.TempData["FlashMessage.Text"] = text;
        }
    }
}
