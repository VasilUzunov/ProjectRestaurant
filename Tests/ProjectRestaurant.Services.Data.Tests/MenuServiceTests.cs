namespace ProjectRestaurant.Services.Data.Tests
{
    using System;
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
    using ProjectRestaurant.Web.ViewModels.Menu;
    using Xunit;

    public class MenuServiceTests
    {
        private readonly IDeletableEntityRepository<MenuItem> menuRepository;
        private readonly IRepository<Category> categoryRepository;
        private readonly IMenuService menuService;

        public MenuServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            this.menuRepository = new EfDeletableEntityRepository<MenuItem>(context);
            this.categoryRepository = new EfRepository<Category>(context);
            this.menuService = new MenuService(this.menuRepository, this.categoryRepository);
        }

        [Fact]
        public async Task CreateMenuItemAsyncShouldWorkAsIsExpected()
        {
            const string name = "Beer";
            const string description = "oneoneoneone oneoneoneone oneoneoneone oneoneoneone";
            const double price = 12.50;
            const double portionWeight = 500.1;
            const string imagePath = "123456789.png";
            const string contentMessage = "Hello World from a Fake File";
            const string categoryName = "Drinks";

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

            var model = new MenuInputModel
            {
                CategoryName = categoryName,
                Description = description,
                Name = name,
                PortionWeight = portionWeight,
                Price = price,
                Image = file,
            };

            var totalObjectsBeforeAdding = this.menuRepository.AllAsNoTracking().Count();

            await this.menuService.CreateAsyncMenuItem(model, "wwwroot/images");

            var totalObjectsAfterAdding = this.menuRepository.AllAsNoTracking().Count();

            var actualObject = await this.menuRepository.GetByIdWithDeletedAsync(1);
            Assert.NotNull(actualObject);

            Assert.Equal(name, actualObject.Name);
            Assert.Equal(description, actualObject.Description);
            Assert.Equal(price, actualObject.Price);
            Assert.Equal(portionWeight, actualObject.PortionWeight);
            Assert.Equal(categoryName, actualObject.Category.Name);
            Assert.Equal("png", actualObject.MenuImage.Extension);

            Assert.Equal(0, totalObjectsBeforeAdding);
            Assert.Equal(1, totalObjectsAfterAdding);
        }

        [Fact]
        public async Task CreateMenuItemAsyncShouldCheckCategory()
        {
            const string name = "Beer";
            const string description = "oneoneoneone oneoneoneone oneoneoneone oneoneoneone";
            const double price = 12.50;
            const double portionWeight = 500.1;
            const string imagePath = "123456789.png";
            const string contentMessage = "Hello World from a Fake File";
            const string categoryName = "Drinks";

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

            var model = new MenuInputModel
            {
                CategoryName = categoryName,
                Description = description,
                Name = name,
                PortionWeight = portionWeight,
                Price = price,
                Image = file,
            };

            await this.menuService.CreateAsyncMenuItem(model, "wwwroot/images");

            var fileMock2 = new Mock<IFormFile>();
            var content2 = contentMessage;
            var fileName2 = "123456789.jpg";
            var ms2 = new MemoryStream();
            var writer2 = new StreamWriter(ms);
            writer2.Write(content);
            writer2.Flush();
            ms2.Position = 0;
            fileMock2.Setup(_ => _.OpenReadStream()).Returns(ms2);
            fileMock2.Setup(_ => _.FileName).Returns(fileName2);
            fileMock2.Setup(_ => _.Length).Returns(ms2.Length);
            var file2 = fileMock2.Object;

            var model2 = new MenuInputModel
            {
                CategoryName = categoryName,
                Description = description + "aaa",
                Name = name + "aaa",
                PortionWeight = portionWeight,
                Price = price + 22.2,
                Image = file2,
            };

            await this.menuService.CreateAsyncMenuItem(model2, "wwwroot/images");

            var totalObjectsAfterAdding = this.menuRepository.AllAsNoTracking().Count();

            var actualObject = await this.menuRepository.GetByIdWithDeletedAsync(2);
            Assert.NotNull(actualObject);

            Assert.Equal(name + "aaa", actualObject.Name);
            Assert.Equal(description + "aaa", actualObject.Description);
            Assert.Equal(price + 22.2, actualObject.Price);
            Assert.Equal(categoryName, actualObject.Category.Name);
            Assert.Equal("jpg", actualObject.MenuImage.Extension);

            Assert.Equal(2, totalObjectsAfterAdding);
        }

        [Fact]
        public void CreateMenuItemAsyncShouldReturnMessage()
        {
            const string name = "Beer";
            const string description = "oneoneoneone oneoneoneone oneoneoneone oneoneoneone";
            const double price = 12.50;
            const double portionWeight = 500.1;
            const string imagePath = "123456789.pdf";
            const string contentMessage = "Hello World from a Fake File";
            const string categoryName = "Drinks";

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

            var model = new MenuInputModel
            {
                CategoryName = categoryName,
                Description = description,
                Name = name,
                PortionWeight = portionWeight,
                Price = price,
                Image = file,
            };

            var totalObjectsBeforeAdding = this.menuRepository.AllAsNoTracking().Count();

            var ex = this.menuService.CreateAsyncMenuItem(model, "wwwroot/images");

            Assert.Equal("Invalid image extension pdf", ex.Exception.InnerException.Message);

            var totalObjectsAfterAdding = this.menuRepository.AllAsNoTracking().Count();

            Assert.Equal(totalObjectsBeforeAdding, totalObjectsAfterAdding);
        }

        [Fact]
        public void GetAllShouldWorkAsIsExpected()
        {
            var mockMenuRepository = new Mock<IDeletableEntityRepository<MenuItem>>();
            var mockCategoryRepository = new Mock<IRepository<Category>>();

            mockCategoryRepository.Setup(x => x.AllAsNoTracking())
                .Returns(new List<Category>()
                {
                    new Category()
                    {
                        Id = 1,
                        Name = "Drinks",
                    },
                }.AsQueryable());

            mockMenuRepository.Setup(x => x.AllAsNoTracking())
                .Returns(new List<MenuItem>()
                {
                    new MenuItem()
                    {
                        Name = "Beer",
                        PortionWeight = 500,
                        Category = new Category { Id = 1, Name = "Drinks" },
                        Price = 12.50,
                        MenuImage = new MenuImage{Id = "klsajldksadklsadsdlkjsadklj", Extension = "png" },
                        Description = "oneoneoneone oneoneoneone oneoneoneone oneoneoneone",
                    },
                    new MenuItem()
                    {
                        Name = "Cake",
                        PortionWeight = 600,
                        Category = new Category { Id = 2, Name = "Deserts" },
                        Price = 15.50,
                        MenuImage = new MenuImage(),
                        Description = "oneoneoneone oneoneoneone oneoneoneone oneoneoneone",
                    },
                }.AsQueryable());

            var menuService = new MenuService(mockMenuRepository.Object, mockCategoryRepository.Object);

            var allItems = menuService.GetAll<MenuViewModel>();

            Assert.Equal(2, allItems.Count());

            foreach (var item in allItems)
            {
                Assert.Equal("oneoneoneone oneoneoneone oneoneoneone oneoneoneone", item.Description);
                Assert.Equal("/images/menu/klsajldksadklsadsdlkjsadklj.png", item.ImageUrl);
                Assert.Equal("Beer", item.Name);
                Assert.Equal(500, item.PortionWeight);
                Assert.Equal(12.50, item.Price);
                Assert.Equal("Drinks", item.CategoryName);
                break;
            }
        }
    }
}
