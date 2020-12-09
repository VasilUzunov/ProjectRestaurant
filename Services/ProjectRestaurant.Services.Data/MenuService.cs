using ProjectRestaurant.Data.Common.Models;

namespace ProjectRestaurant.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using ProjectRestaurant.Data.Common.Repositories;
    using ProjectRestaurant.Data.Models;
    using ProjectRestaurant.Services.Mapping;
    using ProjectRestaurant.Web.ViewModels.Administration;

    public class MenuService : IMenuService
    {
        private readonly IDeletableEntityRepository<MenuItem> menuRepository;
        private readonly IRepository<Category> categoryRepository;
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };

        public MenuService(IDeletableEntityRepository<MenuItem> menuRepository, IRepository<Category> categoryRepository)
        {
            this.menuRepository = menuRepository;
            this.categoryRepository = categoryRepository;
        }

        public async Task CreateAsyncMenuItem(MenuInputModel input, string imagePath)
        {
            var menu = new MenuItem
            {
                Name = input.Name,
                Description = input.Description,
                Price = input.Price,
                PortionWeight = input.PortionWeight,
            };

            Directory.CreateDirectory($"{imagePath}/menu/");
            var extension = Path.GetExtension(input.Image.FileName).TrimStart('.');
            if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            {
                throw new Exception($"Invalid image extension {extension}");
            }

            var dbImage = new MenuImage()
            {
                Extension = extension,
            };
            menu.MenuImage = dbImage;

            var physicalPath = $"{imagePath}/menu/{dbImage.Id}.{extension}";
            await using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await input.Image.CopyToAsync(fileStream);

            var category = this.categoryRepository.AllAsNoTracking().FirstOrDefault(x => x.Name == input.CategoryName);

            if (category == null)
            {
                var categoryName = new Category
                {
                    Name = input.CategoryName,
                };
                menu.Category = categoryName;
            }
            else
            {
                menu.CategoryId = category.Id;
            }

            await this.menuRepository.AddAsync(menu);
            await this.menuRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            var allEvents = this.menuRepository.AllAsNoTracking()
                .To<T>()
                .ToList();

            return allEvents;
        }
    }
}
