namespace ProjectRestaurant.Data.Models
{
    using System;

    using ProjectRestaurant.Data.Common.Models;

    public class EventImage : BaseModel<string>
    {
        public EventImage()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public int EventId { get; set; }

        public virtual Event Event { get; set; }

        public string Extension { get; set; }

        //// The contents of the image is in the file system

        public string RemoteImageUrl { get; set; }
    }
}
