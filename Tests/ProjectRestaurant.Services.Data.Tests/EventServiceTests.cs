namespace ProjectRestaurant.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Moq;
    using ProjectRestaurant.Data.Common.Repositories;
    using ProjectRestaurant.Data.Models;
    using ProjectRestaurant.Data.Repositories;
    using ProjectRestaurant.Services.Data.Tests.Common;
    using ProjectRestaurant.Services.Mapping;
    using ProjectRestaurant.Web.ViewModels;
    using ProjectRestaurant.Web.ViewModels.Administration;
    using ProjectRestaurant.Web.ViewModels.Event;
    using Xunit;

    public class EventServiceTests
    {
        private readonly IDeletableEntityRepository<Event> eventRepository;
        private readonly IEventService eventService;

        public EventServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            this.eventRepository = new EfDeletableEntityRepository<Event>(context);
            this.eventService = new EventService(this.eventRepository);
        }

        [Fact]
        public async Task CreateEventAsyncShouldWorkAsIsExpected()
        {
            const string name = "Birthday";
            const string description = "oneoneoneone oneoneoneone oneoneoneone oneoneoneone";
            const double price = 12.50;
            const string imagePath = "123456789.png";
            const string contentMessage = "Hello World from a Fake File";

            var fileMock = new Mock<IFormFile>();
            var content = contentMessage;
            var fileName = imagePath;
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);
            var file = fileMock.Object;

            var model = new EventInputModel()
            {
                Description = description,
                Name = name,
                Price = price,
                Image = file,
            };

            var totalObjectsBeforeAdding = this.eventRepository.AllAsNoTracking().Count();

            await this.eventService.CreateAsyncEvent(model, "wwwroot/images");

            var totalObjectsAfterAdding = this.eventRepository.AllAsNoTracking().Count();

            var actualObject = await this.eventRepository.GetByIdWithDeletedAsync(1);
            Assert.NotNull(actualObject);

            Assert.Equal(name, actualObject.Name);
            Assert.Equal(description, actualObject.Description);
            Assert.Equal(price, actualObject.Price);
            Assert.Equal("png", actualObject.EventImage.Extension);

            Assert.Equal(0, totalObjectsBeforeAdding);
            Assert.Equal(1, totalObjectsAfterAdding);
        }

        [Fact]
        public void CreateEventAsyncShouldCheckCategory()
        {
            const string name = "Birthday";
            const string description = "oneoneoneone oneoneoneone oneoneoneone oneoneoneone";
            const double price = 12.50;
            const string imagePath = "123456789.pdf";
            const string contentMessage = "Hello World from a Fake File";

            var fileMock = new Mock<IFormFile>();
            var content = contentMessage;
            var fileName = imagePath;
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);
            var file = fileMock.Object;

            var model = new EventInputModel()
            {
                Description = description,
                Name = name,
                Price = price,
                Image = file,
            };

            var totalObjectsBeforeAdding = this.eventRepository.AllAsNoTracking().Count();

            var ex = this.eventService.CreateAsyncEvent(model, "wwwroot/images");

            Assert.Equal("Invalid image extension pdf", ex.Exception.InnerException.Message);

            var totalObjectsAfterAdding = this.eventRepository.AllAsNoTracking().Count();

            Assert.Equal(totalObjectsBeforeAdding, totalObjectsAfterAdding);
        }

        [Fact]
        public void GetAllShouldWorkAsIsExpected()
        {
            var mockEventRepository = new Mock<IDeletableEntityRepository<Event>>();

            mockEventRepository.Setup(x => x.AllAsNoTracking())
                .Returns(new List<Event>()
                {
                    new Event()
                    {
                        Name = "Birthday",
                        Price = 12.50,
                        EventImage = new EventImage{Id = "klsajldksadklsadsdlkjsadklj", Extension = "png" },
                        Description = "oneoneoneone oneoneoneone oneoneoneone oneoneoneone",
                    },
                    new Event()
                    {
                        Name = "New Year",
                        Price = 15.50,
                        EventImage = new EventImage{Id = "klsajldksadklsadsdlkjsadklj", Extension = "png" },
                        Description = "oneoneoneone oneoneoneone oneoneoneone oneoneoneone",
                    },
                }.AsQueryable());

            var eventService = new EventService(mockEventRepository.Object);

            var allItems = eventService.GetAll<EventViewModel>();

            Assert.Equal(2, allItems.Count());

            foreach (var item in allItems)
            {
                Assert.Equal("oneoneoneone oneoneoneone oneoneoneone oneoneoneone", item.Description);
                Assert.Equal("/images/events/klsajldksadklsadsdlkjsadklj.png", item.ImageUrl);
                Assert.Equal("Birthday", item.Name);
                Assert.Equal(12.50, item.Price);
                break;
            }
        }
    }
}
