namespace ProjectRestaurant.Services.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;

    using ProjectRestaurant.Data.Common.Repositories;
    using ProjectRestaurant.Data.Models;
    using ProjectRestaurant.Services.Mapping;
    using ProjectRestaurant.Web.ViewModels.Administration;

    public class EventService : IEventService
    {
        private readonly IDeletableEntityRepository<Event> eventRepository;
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };

        public EventService(IDeletableEntityRepository<Event> eventRepository)
        {
            this.eventRepository = eventRepository;
        }

        public async Task CreateAsyncEvent(EventInputModel input, string imagePath)
        {
            var createEvent = new Event
            {
                Name = input.Name,
                Price = input.Price,
                Description = input.Description,
            };

            Directory.CreateDirectory($"{imagePath}/events/");
            var extension = Path.GetExtension(input.Image.FileName).TrimStart('.');
            if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            {
                throw new Exception($"Invalid image extension {extension}");
            }

            var dbImage = new EventImage()
            {
                Extension = extension,
            };
            createEvent.EventImage = dbImage;

            var physicalPath = $"{imagePath}/events/{dbImage.Id}.{extension}";
            await using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await input.Image.CopyToAsync(fileStream);

            await this.eventRepository.AddAsync(createEvent);
            await this.eventRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            var allEvents = this.eventRepository.AllAsNoTracking()
                .To<T>()
                .ToList();

            return allEvents;
        }
    }
}
