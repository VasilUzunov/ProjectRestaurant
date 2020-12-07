namespace ProjectRestaurant.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using ProjectRestaurant.Web.ViewModels.Reservation;

    public interface IReservationsService
    {
        Task CreateAsyncMenuItem(ReservationInputModel input);

        IEnumerable<T> GetAll<T>();
    }
}
