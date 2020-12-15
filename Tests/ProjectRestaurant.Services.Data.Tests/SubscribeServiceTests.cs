namespace ProjectRestaurant.Services.Data.Tests
{
    using System.Linq;
    using System.Threading.Tasks;

    using ProjectRestaurant.Data.Common.Repositories;
    using ProjectRestaurant.Data.Models;
    using ProjectRestaurant.Data.Repositories;
    using ProjectRestaurant.Services.Data.Tests.Common;
    using Xunit;

    public class SubscribeServiceTests
    {
        private readonly IRepository<Subscribe> subscribeRepository;
        private readonly ISubscribeService subscribeService;

        public SubscribeServiceTests()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            this.subscribeRepository = new EfRepository<Subscribe>(context);
            this.subscribeService = new SubscribeService(this.subscribeRepository);
        }

        [Fact]
        public async Task SetVoteAsyncShouldWorkAsIsExpected()
        {
            const string email = "email@email.com";

            var totalObjectsBeforeAdding = this.subscribeRepository.AllAsNoTracking().Count();

            await this.subscribeService.AddAsyncSubscriber(email);

            var totalObjectsAfterAdding = this.subscribeRepository.AllAsNoTracking().Count();

            var actualObject = this.subscribeRepository.AllAsNoTracking().ToList();

            Assert.NotNull(actualObject);

            Assert.Equal(email, actualObject.First().Email);

            Assert.Equal(0, totalObjectsBeforeAdding);
            Assert.Equal(1, totalObjectsAfterAdding);
        }
    }
}
