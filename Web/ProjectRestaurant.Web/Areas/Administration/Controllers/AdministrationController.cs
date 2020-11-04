namespace ProjectRestaurant.Web.Areas.Administration.Controllers
{
    using ProjectRestaurant.Common;
    using ProjectRestaurant.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
