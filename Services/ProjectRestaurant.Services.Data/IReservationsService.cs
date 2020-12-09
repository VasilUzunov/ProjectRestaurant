namespace ProjectRestaurant.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using ProjectRestaurant.Web.ViewModels.Reservation;

    public interface IReservationsService
    {
        Task CreateAsyncReservation(ReservationInputModel input, string userId);

        IEnumerable<T> GetAll<T>();

        string GetPhoneNumber(string userId);
    }
}
