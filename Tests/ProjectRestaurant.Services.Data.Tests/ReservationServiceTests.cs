using System;

namespace ProjectRestaurant.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Moq;
    using ProjectRestaurant.Data.Common.Repositories;
    using ProjectRestaurant.Data.Models;
    using ProjectRestaurant.Data.Repositories;
    using ProjectRestaurant.Services.Data.Tests.Common;
    using ProjectRestaurant.Services.Mapping;
    using ProjectRestaurant.Web.ViewModels;
    using ProjectRestaurant.Web.ViewModels.Administration;
    using ProjectRestaurant.Web.ViewModels.Reservation;
    using Xunit;

    public class ReservationServiceTests
    {
        private readonly IDeletableEntityRepository<Reservation> reservationRepository;
        private readonly IDeletableEntityRepository<Table> tableRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IReservationsService reservationsService;
        private readonly ITableService tableService;

        public ReservationServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            this.reservationRepository = new EfDeletableEntityRepository<Reservation>(context);
            this.tableRepository = new EfDeletableEntityRepository<Table>(context);
            this.userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            this.reservationsService = new ReservationsService(this.reservationRepository, this.tableRepository, this.userRepository);
            this.tableService = new TableService(this.tableRepository);
        }

        [Fact]
        public async Task CreateReservationAsyncShouldWorkAsIsExpected()
        {
            const string message = "Beerbeerrrrrrrjkhsajkdskdasdalkjsasadjkldsa";
            const string date = "12/12/2020";
            const string hour = "12";
            const string minutes = "12";
            const int table = 3;
            const int numberOfPeople = 3;
            const string userId = "68a7b421-feca-40b2-bece-ecaf81f6ebb8";

            var model = new ReservationInputModel()
            {
                Message = message,
                Date = date,
                Hour = hour,
                Table = table,
                Minutes = minutes,
                NumberOfPeople = numberOfPeople,
            };

            await this.tableService.CreateAsyncTable(new TableInputModel
            {
                ShapeOfTable = "oval",
                NumberOfSeats = numberOfPeople,
                NumberOfTable = table,
            });

            var totalObjectsBeforeAdding = this.reservationRepository.AllAsNoTracking().Count();

            await this.reservationsService.CreateAsyncReservation(model, userId);

            var totalObjectsAfterAdding = this.reservationRepository.AllAsNoTracking().Count();

            var actualObject = await this.reservationRepository.GetByIdWithDeletedAsync(1);
            Assert.NotNull(actualObject);

            Assert.Equal(message, actualObject.Message);
            Assert.Equal(userId, actualObject.UserId);
            Assert.Equal(table, actualObject.Table.TableNumber);
            Assert.Equal(numberOfPeople, actualObject.Table.NumberOfSeats);
            Assert.Equal($"{date} {hour}:{minutes}:00 PM", actualObject.DateAndTimeOfReservation.ToString());

            Assert.Equal(0, totalObjectsBeforeAdding);
            Assert.Equal(1, totalObjectsAfterAdding);
        }

        //[Fact]
        //public void GetAllShouldWorkAsIsExpected()
        //{
        //    var mockReservationRepository = new Mock<IDeletableEntityRepository<Reservation>>();
        //    var mockTableRepository = new Mock<IDeletableEntityRepository<Table>>();
        //    var mockUserRepository = new Mock<IDeletableEntityRepository<ApplicationUser>>();

        //    mockTableRepository.Setup(x => x.AllAsNoTracking())
        //        .Returns(new List<Table>()
        //        {
        //            new Table()
        //            {
        //                NumberOfSeats = 3,
        //                ShapeOfTable = "oval",
        //                TableNumber = 3,
        //            },
        //        }.AsQueryable());

        //    mockUserRepository.Setup(x => x.AllAsNoTracking())
        //        .Returns(new List<ApplicationUser>()
        //        {
        //            new ApplicationUser()
        //            {
        //                Address = "nsakjsalkjalkjAJASlkajSLKASjaslKJSAklas",
        //                Email = "vasil@abv.bg",
        //                FirstName = "Vasil",
        //                PhoneNumber = "0877980616",
        //                LastName = "Uzunov",
        //                PasswordHash = "AQAAAAEAACcQAAAAELHKxwpRjNH7bxS7xiHAhy95SNG/7le/06QkI0TM8kmjFGUlueU+iS+TnCaogfjRzw==",
        //            },
        //        }.AsQueryable());

        //    mockReservationRepository.Setup(x => x.AllAsNoTracking())
        //        .Returns(new List<Reservation>()
        //        {
        //            new Reservation()
        //            {
        //                UserId = mockUserRepository.Object.AllAsNoTrackingWithDeleted().First().Id,
        //                Message = "kjsakjhdsjkhsakjsahsadkjhsakhjsadk",
        //                TableId = mockTableRepository.Object.AllAsNoTrackingWithDeleted().First().Id,
        //                DateAndTimeOfReservation = DateTime.Now,
        //            },
        //        }.AsQueryable());

        //    var reservationService = new ReservationsService (mockReservationRepository.Object, mockTableRepository.Object, mockUserRepository.Object);

        //    var allItems = reservationService.GetAll<ReservationViewModel>();

        //    Assert.Equal(2, allItems.Count());

        //    foreach (var item in allItems)
        //    {
        //        Assert.Equal("oneoneoneone oneoneoneone oneoneoneone oneoneoneone", item.Description);
        //        Assert.Equal("/images/menu/klsajldksadklsadsdlkjsadklj.png", item.ImageUrl);
        //        Assert.Equal("Beer", item.Name);
        //        Assert.Equal(500, item.PortionWeight);
        //        Assert.Equal(12.50, item.Price);
        //        Assert.Equal("Drinks", item.CategoryName);
        //        break;
        //    }
        //}

        [Fact]
        public void GetPhoneNumberShouldWorkAsIsExpected()
        {
            var mockReservationRepository = new Mock<IDeletableEntityRepository<Reservation>>();
            var mockTableRepository = new Mock<IDeletableEntityRepository<Table>>();
            var mockUserRepository = new Mock<IDeletableEntityRepository<ApplicationUser>>();

            mockUserRepository.Setup(x => x.AllAsNoTracking())
                .Returns(new List<ApplicationUser>()
                {
                        new ApplicationUser()
                        {
                            Id = "1d1a2708-fd4c-422a-bf1a-22bd1a2e55e3",
                            Address = "nsakjsalkjalkjAJASlkajSLKASjaslKJSAklas",
                            Email = "vasil@abv.bg",
                            FirstName = "Vasil",
                            PhoneNumber = "0877980616",
                            LastName = "Uzunov",
                            PasswordHash = "AQAAAAEAACcQAAAAELHKxwpRjNH7bxS7xiHAhy95SNG/7le/06QkI0TM8kmjFGUlueU+iS+TnCaogfjRzw==",
                        },
                }.AsQueryable());

            var reservationService = new ReservationsService(mockReservationRepository.Object, mockTableRepository.Object, mockUserRepository.Object);

            var allItems = reservationService.GetPhoneNumber("1d1a2708-fd4c-422a-bf1a-22bd1a2e55e3");

            Assert.Equal(10, allItems.Length);
            Assert.Equal("0877980616", allItems);
        }
    }
}
