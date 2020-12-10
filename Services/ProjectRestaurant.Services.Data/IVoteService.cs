namespace ProjectRestaurant.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ProjectRestaurant.Web.ViewModels.Vote;

    public interface IVoteService
    {
        Task SetVoteAsync(string userId, int star, string description);

        IEnumerable<T> GetAll<T>();
    }
}
