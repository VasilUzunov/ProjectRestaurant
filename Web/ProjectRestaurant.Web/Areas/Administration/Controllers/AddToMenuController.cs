namespace ProjectRestaurant.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using ProjectRestaurant.Services.Data;
    using ProjectRestaurant.Web.ViewModels.Administration;

    public class AddToMenuController : AdministrationController
    {
        private readonly IMenuService menuService;
        private readonly IWebHostEnvironment environment;

        public AddToMenuController(IMenuService menuService, IWebHostEnvironment environment)
        {
            this.menuService = menuService;
            this.environment = environment;
        }

        public IActionResult AddToMenu()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddToMenu(MenuInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.menuService.CreateAsyncMenuItem(input, $"{this.environment.WebRootPath}/images");
            return this.Redirect("/Administration/AddToMenu/AddToMenu");
        }
    }
}
