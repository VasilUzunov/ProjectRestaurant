namespace ProjectRestaurant.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ProjectRestaurant.Web.ViewModels.Administration;

    public interface ITableService
    {
        Task CreateAsyncTable(TableInputModel input);

        IEnumerable<T> GetAll<T>();
    }
}
