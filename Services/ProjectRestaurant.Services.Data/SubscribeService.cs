namespace ProjectRestaurant.Services.Data
{
    using System.Threading.Tasks;

    using ProjectRestaurant.Data.Common.Repositories;
    using ProjectRestaurant.Data.Models;

    public class SubscribeService : ISubscribeService
    {
        private readonly IRepository<Subscribe> subscribeRepository;

        public SubscribeService(IRepository<Subscribe> subscribeRepository)
        {
            this.subscribeRepository = subscribeRepository;
        }

        public async Task AddAsyncSubscriber(string email)
        {
            var subscribe = new Subscribe
            {
                Email = email,
            };

            await this.subscribeRepository.AddAsync(subscribe);
            await this.subscribeRepository.SaveChangesAsync();
        }
    }
}
