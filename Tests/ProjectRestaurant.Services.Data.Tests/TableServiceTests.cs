namespace ProjectRestaurant.Services.Data.Tests
{
    using System.Linq;
    using System.Threading.Tasks;

    using ProjectRestaurant.Data.Common.Repositories;
    using ProjectRestaurant.Data.Models;
    using ProjectRestaurant.Data.Repositories;
    using ProjectRestaurant.Services.Data.Tests.Common;
    using ProjectRestaurant.Web.ViewModels.Administration;
    using Xunit;

    public class TableServiceTests
    {
        private readonly IDeletableEntityRepository<Table> tableRepository;
        private readonly ITableService tableService;

        public TableServiceTests()
        {
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
    }
}
