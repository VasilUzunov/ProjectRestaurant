namespace ProjectRestaurant.Data.Models
{
    using System;

    using ProjectRestaurant.Data.Common.Models;

    public class MenuImage : BaseModel<string>
    {
        public MenuImage()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public int MenuItemId { get; set; }

        public virtual MenuItem MenuItem { get; set; }

        public string Extension { get; set; }

        //// The contents of the image is in the file system

        public string RemoteImageUrl { get; set; }
    }
}
