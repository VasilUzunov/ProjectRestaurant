namespace ProjectRestaurant.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Moq;
    using ProjectRestaurant.Data.Common.Repositories;
    using ProjectRestaurant.Data.Models;
    using ProjectRestaurant.Data.Repositories;
    using ProjectRestaurant.Services.Data.Tests.Common;
    using ProjectRestaurant.Services.Mapping;
    using ProjectRestaurant.Web.ViewModels;
    using ProjectRestaurant.Web.ViewModels.Vote;
    using Xunit;

    public class VoteServiceTests
    {
        private readonly IDeletableEntityRepository<Rating> voteRepository;
        private readonly IVoteService voteService;

        public VoteServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            this.voteRepository = new EfDeletableEntityRepository<Rating>(context);
            this.voteService = new VoteService(this.voteRepository);
        }

        [Fact]
        public async Task SetVoteAsyncShouldWorkAsIsExpected()
        {
            const string userId = "68a7b421-feca-40b2-bece-ecaf81f6ebb8";
            const string description = "oneoneoneone oneoneoneone oneoneoneone oneoneoneone";
            const int stars = 3;

            var totalObjectsBeforeAdding = this.voteRepository.AllAsNoTracking().Count();

            await this.voteService.SetVoteAsync(userId, stars, description);

            var totalObjectsAfterAdding = this.voteRepository.AllAsNoTracking().Count();

            var actualObject = await this.voteRepository.GetByIdWithDeletedAsync(1);
            Assert.NotNull(actualObject);

            Assert.Equal(userId, actualObject.UserId);
            Assert.Equal(description, actualObject.Description);
            Assert.Equal(stars, actualObject.Star);

            Assert.Equal(0, totalObjectsBeforeAdding);
            Assert.Equal(1, totalObjectsAfterAdding);
        }

        [Fact]
        public void GetAllShouldWorkAsIsExpected()
        {
            var mockRepository = new Mock<IDeletableEntityRepository<Rating>>();

            mockRepository.Setup(x => x.AllAsNoTracking())
                .Returns(new List<Rating>()
                {
                    new Rating
                    {
                        Id = 1,
                        User = new ApplicationUser(),
                        Description = "oneoneoneone oneoneoneone oneoneoneone oneoneoneone",
                        Star = 5,
                    },
                    new Rating
                    {
                        Id = 2,
                        User = new ApplicationUser(),
                        Description = "oneoneoneone oneoneoneone oneoneoneone oneoneoneone",
                        Star = 4,
                    },
                }.AsQueryable());

            var voteService = new VoteService(mockRepository.Object);

            var allVotes = voteService.GetAll<VoteViewModel>();

            Assert.Equal(2, allVotes.Count());

            foreach (var vote in allVotes)
            {
                Assert.Equal("oneoneoneone oneoneoneone oneoneoneone oneoneoneone", vote.Description);
                Assert.Equal(5, vote.Star);
                break;
            }
        }
    }
}
