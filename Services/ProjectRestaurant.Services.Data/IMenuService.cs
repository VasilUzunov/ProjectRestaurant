namespace ProjectRestaurant.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ProjectRestaurant.Web.ViewModels.Administration;

    public interface IMenuService
    {
        Task CreateAsyncMenuItem(MenuInputModel input, string imagePath);

        IEnumerable<T> GetAll<T>();
    }
}
