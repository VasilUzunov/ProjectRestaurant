using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(ProjectRestaurant.Web.Areas.Identity.IdentityHostingStartup))]

namespace ProjectRestaurant.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}