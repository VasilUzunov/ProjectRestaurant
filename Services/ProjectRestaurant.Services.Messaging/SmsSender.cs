namespace ProjectRestaurant.Services.Messaging
{
    using System;
    using System.Threading.Tasks;

    using Twilio;
    using Twilio.Exceptions;
    using Twilio.Rest.Api.V2010.Account;
    using Twilio.Types;

    public class SmsSender : ISmsSender
    {
        private readonly TwilioClient client;

        public SmsSender(string sid,string authToken)
        {
            TwilioClient.Init(sid, authToken);
        }

        public Task SendEmailAsync(string toPhone, string message)
        {
            if (string.IsNullOrWhiteSpace(message) && string.IsNullOrWhiteSpace(toPhone))
            {
                throw new ArgumentException("Subject and message should be provided.");
            }

            try
            {
                // Send an SMS message.
                var sms = MessageResource.CreateAsync(
                    to: new PhoneNumber(toPhone),
                    from: new PhoneNumber("+17658964505"),
                    body: message);
            }
            catch (TwilioException ex)
            {
                // An exception occurred making the REST call
                Console.WriteLine(ex.Message);
                throw;
            }

            return Task.CompletedTask;
        }
    }
}
