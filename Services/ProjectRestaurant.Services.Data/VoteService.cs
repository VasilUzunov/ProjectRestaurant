namespace ProjectRestaurant.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ProjectRestaurant.Data.Common.Repositories;
    using ProjectRestaurant.Data.Models;
    using ProjectRestaurant.Services.Mapping;

    public class VoteService : IVoteService
    {
        private readonly IDeletableEntityRepository<Rating> raitingRepository;

        public VoteService(IDeletableEntityRepository<Rating> raitingRepository)
        {
            this.raitingRepository = raitingRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var votes = this.raitingRepository.AllAsNoTracking().To<T>().ToList();
            return votes;
        }

        public async Task SetVoteAsync(string userId, int star, string description)
        {
            var vote = this.raitingRepository.All()
                .FirstOrDefault(x => x.UserId == userId);
            if (vote == null)
            {
                vote = new Rating
                {
                    Star = star,
                    Description = description,
                    UserId = userId,
                };

                await this.raitingRepository.AddAsync(vote);
                await this.raitingRepository.SaveChangesAsync();
            }
        }
    }
}
