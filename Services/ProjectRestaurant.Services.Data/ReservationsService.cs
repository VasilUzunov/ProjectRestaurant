﻿namespace ProjectRestaurant.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ProjectRestaurant.Data.Common.Repositories;
    using ProjectRestaurant.Data.Models;
    using ProjectRestaurant.Services.Mapping;
    using ProjectRestaurant.Web.ViewModels.Reservation;

    public class ReservationsService : IReservationsService
    {
        private readonly IDeletableEntityRepository<Reservation> reservationRepository;
        private readonly IDeletableEntityRepository<Table> tableRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> user;

        public ReservationsService(IDeletableEntityRepository<Reservation> reservationRepository, IDeletableEntityRepository<Table> tableRepository, IDeletableEntityRepository<ApplicationUser> user)
        {
            this.reservationRepository = reservationRepository;
            this.tableRepository = tableRepository;
            this.user = user;
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
                NumberOfPeople = input.NumberOfPeople,
            };

            await this.reservationRepository.AddAsync(reservation);
            await this.reservationRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            var reservation = this.reservationRepository.AllAsNoTracking().OrderByDescending(x => x.DateAndTimeOfReservation).To<T>().ToList();
            return reservation;
        }

        public IEnumerable<T> GetAll<T>(string userId)
        {
            var reservation = this.reservationRepository.AllAsNoTracking().Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedOn).To<T>().ToList();
            return reservation;
        }

        public string GetPhoneNumber(string userId)
        {
            string phone = this.user.AllAsNoTracking().Where(x => x.Id == userId).Select(x => x.PhoneNumber).First();
            return phone;
        }
    }
}
