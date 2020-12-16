namespace ProjectRestaurant.Services.Messaging
{
    using System.Threading.Tasks;

    public interface ISmsSender
    {
        Task SendSmsAsync(
            string to,
            string message);
    }
}
