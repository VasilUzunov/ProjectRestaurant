namespace ProjectRestaurant.Services.Data
{
    using System.Threading.Tasks;

    public interface ISubscribeService
    {
        Task AddAsyncSubscriber(string email);
    }
}
