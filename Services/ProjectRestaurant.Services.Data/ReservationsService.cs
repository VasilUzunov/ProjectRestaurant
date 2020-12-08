namespace ProjectRestaurant.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ProjectRestaurant.Data.Common.Repositories;
    using ProjectRestaurant.Data.Models;
    using ProjectRestaurant.Web.ViewModels.Reservation;

    public class ReservationsService : IReservationsService
    {
        private readonly IDeletableEntityRepository<Reservation> reservationRepository;
        private readonly IDeletableEntityRepository<Table> tableRepository;

        public ReservationsService(IDeletableEntityRepository<Reservation> reservationRepository, IDeletableEntityRepository<Table> tableRepository)
        {
            this.reservationRepository = reservationRepository;
            this.tableRepository = tableRepository;
        }

        public async Task CreateAsyncReservation(ReservationInputModel input, string userId)
        {
            string date = input.Date + " " + input.Hour + ":" + input.Minutes;
            var dateAndTime = DateTime.Parse(date);
            var table = this.tableRepository.AllAsNoTracking().First(x => x.TableNumber == input.Table);

            var reservation = new Reservation
            {
                Message = input.Message,
                UserId = userId,
                DateAndTimeOfReservation = dateAndTime,
                TableId = table.Id,
            };

            await this.reservationRepository.AddAsync(reservation);
            await this.reservationRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            throw new System.NotImplementedException();
        }
    }
}
