namespace ProjectRestaurant.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using ProjectRestaurant.Services.Data;
    using ProjectRestaurant.Web.ViewModels.Administration;

    public class TableController : AdministrationController
    {
        private readonly ITableService tableService;

        public TableController(ITableService tableService)
        {
            this.tableService = tableService;
        }

        public IActionResult AddTables()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTables(TableInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.tableService.CreateAsyncTable(input);
            return this.View();
        }

        public IActionResult AllTables()
        {
            var model = this.tableService.GetAll<TableViewModel>();
            return this.View(model);
        }
    }
}
