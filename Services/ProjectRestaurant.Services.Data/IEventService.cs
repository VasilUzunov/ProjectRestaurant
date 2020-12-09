namespace ProjectRestaurant.Services.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ProjectRestaurant.Web.ViewModels.Administration;

    public interface IEventService
    {
        Task CreateAsyncEvent(EventInputModel input, string imagePath);

        IEnumerable<T> GetAll<T>();
    }
}
