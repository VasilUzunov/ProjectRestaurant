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
    using Xunit;

    public class TableServiceTests
    {
        private readonly IDeletableEntityRepository<Table> tableRepository;
        private readonly ITableService tableService;

        public TableServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            this.tableRepository = new EfDeletableEntityRepository<Table>(context);
            this.tableService = new TableService(this.tableRepository);
        }

        [Fact]
        public async Task CreateTableAsyncShouldWorkAsIsExpected()
        {
            const int numberOfSeats = 3;
            const string shapeOfTable = "oval";
            const int numberOfTable = 3;

            var totalObjectsBeforeAdding = this.tableRepository.AllAsNoTracking().Count();

            await this.tableService.CreateAsyncTable(new TableInputModel
            {
                ShapeOfTable = shapeOfTable,
                NumberOfSeats = numberOfSeats,
                NumberOfTable = numberOfTable,
            });

            var totalObjectsAfterAdding = this.tableRepository.AllAsNoTracking().Count();

            var actualObject = await this.tableRepository.GetByIdWithDeletedAsync(1);
            Assert.NotNull(actualObject);

            Assert.Equal(numberOfSeats, actualObject.NumberOfSeats);
            Assert.Equal(numberOfTable, actualObject.TableNumber);
            Assert.Equal(shapeOfTable, actualObject.ShapeOfTable);

            Assert.Equal(0, totalObjectsBeforeAdding);
            Assert.Equal(1, totalObjectsAfterAdding);
        }

        [Fact]
        public void GetAllShouldWorkAsIsExpected()
        {
            var mockTableRepository = new Mock<IDeletableEntityRepository<Table>>();

            mockTableRepository.Setup(x => x.AllAsNoTracking())
                .Returns(new List<Table>()
                {
                    new Table()
                    {
                        ShapeOfTable = "Oval",
                        NumberOfSeats = 5,
                        TableNumber = 1,
                    },
                    new Table()
                    {
                        ShapeOfTable = "Ellipse",
                        NumberOfSeats = 4,
                        TableNumber = 2,
                    },
                }.AsQueryable());

            var tableService = new TableService(mockTableRepository.Object);

            var allItems = tableService.GetAll<TableViewModel>();

            Assert.Equal(2, allItems.Count());

            foreach (var item in allItems)
            {
                Assert.Equal(1, item.TableNumber);
                Assert.Equal(5, item.NumberOfSeats);
                Assert.Equal("Oval", item.ShapeOfTable);
                break;
            }
        }
    }
}
