namespace ProjectRestaurant.Web.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using ProjectRestaurant.Services.Data;
    using ProjectRestaurant.Web.ViewModels.Menu;

    public class MenuController : BaseController
    {
        private readonly IMenuService menuService;

        public MenuController(IMenuService menuService)
        {
            this.menuService = menuService;
        }

        public IActionResult Menu()
        {
            var menuView = new MenuInListViewModel
            {
                MenuItems = this.menuService.GetAll<MenuViewModel>(),
            };

            return this.View(menuView);
        }

        public IActionResult MenuPartial(string value)
        {
            var menuView = new MenuInListViewModel
            {
                MenuItems = this.menuService.GetAll<MenuViewModel>(),
            };

            switch (value)
            {
                case "all":
                    menuView.MenuItems = menuView.MenuItems;
                    break;
                case "salads":
                    menuView.MenuItems = menuView.MenuItems.Where(x => x.CategoryName == "Salads");
                    break;
                case "specialty":
                    menuView.MenuItems = menuView.MenuItems.Where(x => x.CategoryName == "Specialty");
                    break;
                case "desserts":
                    menuView.MenuItems = menuView.MenuItems.Where(x => x.CategoryName == "Desserts");
                    break;
                case "drinks":
                    menuView.MenuItems = menuView.MenuItems.Where(x => x.CategoryName == "Drinks");
                    break;
            }

            return this.PartialView("_MenuPartial", menuView);
        }
    }
}
