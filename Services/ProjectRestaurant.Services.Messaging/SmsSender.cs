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
        private readonly string phone;

        public SmsSender(string sid, string authToken, string phone)
        {
            this.phone = phone;
            TwilioClient.Init(sid, authToken);
        }

        public Task SendSmsAsync(string toPhone, string message)
        {
            if (string.IsNullOrWhiteSpace(message) || string.IsNullOrWhiteSpace(toPhone))
            {
                throw new ArgumentException("Phone and message should be provided.");
            }

            try
            {
                // Send an SMS message.
                var sms = MessageResource.CreateAsync(
                    to: new PhoneNumber(toPhone),
                    from: new PhoneNumber(this.phone),
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
