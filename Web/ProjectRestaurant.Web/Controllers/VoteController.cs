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
        public async Task Post(VoteInputModel input)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await this.voteService.SetVoteAsync(userId, input.Star, input.Description);
        }
    }
}
