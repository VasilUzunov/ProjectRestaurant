using ProjectRestaurant.Web.ViewModels.Menu;

namespace ProjectRestaurant.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using ProjectRestaurant.Services.Data;
    using ProjectRestaurant.Web.ViewModels.Administration;

    public class MenuController : AdministrationController
    {
        private readonly IMenuService menuService;
        private readonly IWebHostEnvironment environment;

        public MenuController(IMenuService menuService, IWebHostEnvironment environment)
        {
            this.menuService = menuService;
            this.environment = environment;
        }

        public IActionResult AddMenuItems()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddMenuItems(MenuInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.menuService.CreateAsyncMenuItem(input, $"{this.environment.WebRootPath}/images");
            return this.View();
        }

        public IActionResult AllMenuItems()
        {
            var model = this.menuService.GetAll<MenuViewModel>();
            return this.View(model);
        }
    }
}
