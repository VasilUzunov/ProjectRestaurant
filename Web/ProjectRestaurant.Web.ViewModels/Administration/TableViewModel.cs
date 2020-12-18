namespace ProjectRestaurant.Web.ViewModels.Administration
{
    using ProjectRestaurant.Data.Models;
    using ProjectRestaurant.Services.Mapping;

    public class TableViewModel : IMapFrom<Table>
    {
        public int NumberOfSeats { get; set; }

        public string ShapeOfTable { get; set; }

        public int TableNumber { get; set; }
    }
}
