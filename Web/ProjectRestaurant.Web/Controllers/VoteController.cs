namespace ProjectRestaurant.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using ProjectRestaurant.Services.Data;
    using ProjectRestaurant.Web.ViewModels.Vote;

    [ApiController]
    [Route("api/[controller]")]
    public class VoteController : BaseController
    {
        private readonly IVoteService voteService;

        public VoteController(IVoteService voteService)
        {
            this.voteService = voteService;
        }

        [HttpPost]
        public async Task<RedirectToActionResult> Post(VoteInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Post");
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await this.voteService.SetVoteAsync(userId, input.Star, input.Description);
            return (RedirectToActionResult)this.TempData["You successfully vote!"];
        }
    }
}
