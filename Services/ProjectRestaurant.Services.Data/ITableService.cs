namespace ProjectRestaurant.Services.Data
{
    using System.Threading.Tasks;

    using ProjectRestaurant.Web.ViewModels.Administration;

    public interface ITableService
    {
        Task CreateAsyncTable(TableInputModel input);
    }
}
