namespace ProjectRestaurant.Services.Messaging
{
    using System.Threading.Tasks;

    public interface ISmsSender
    {
        Task SendEmailAsync(
            string to,
            string message);
    }
}
