using System.Linq;
using ProjectRestaurant.Services.Mapping;

namespace ProjectRestaurant.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ProjectRestaurant.Data.Common.Repositories;
    using ProjectRestaurant.Data.Models;
    using ProjectRestaurant.Web.ViewModels.Administration;

    public class TableService : ITableService
    {
        private readonly IDeletableEntityRepository<Table> tableRepository;

        public TableService(IDeletableEntityRepository<Table> tableRepository)
        {
            this.tableRepository = tableRepository;
        }

        public async Task CreateAsyncTable(TableInputModel input)
        {
            var table = new Table
            {
                NumberOfSeats = input.NumberOfSeats,
                ShapeOfTable = input.ShapeOfTable,
                TableNumber = input.NumberOfTable,
            };

            await this.tableRepository.AddAsync(table);
            await this.tableRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            var tables = this.tableRepository.AllAsNoTracking().To<T>().ToList();
            return tables;
        }
    }
}
