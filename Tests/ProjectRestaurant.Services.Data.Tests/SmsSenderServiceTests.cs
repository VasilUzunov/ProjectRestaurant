namespace ProjectRestaurant.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;
    using ProjectRestaurant.Common;
    using ProjectRestaurant.Services.Messaging;
    using Twilio.Exceptions;
    using Xunit;

    public class SmsSenderServiceTests
    {
        private readonly ISmsSender smsSenderService;

        public SmsSenderServiceTests()
        {
            this.smsSenderService = new SmsSender(GlobalConstantsForTest.SID, GlobalConstantsForTest.AuthToken, GlobalConstantsForTest.Phone);
        }

        [Fact]
        public void SendEmailAsyncShouldWorkAsIsExpected()
        {
            const string message = "sal;sakl;a;klsakl;as;klas;lksa";
            const string to = "+16666666666";

            Assert.Equal(Task.CompletedTask, this.smsSenderService.SendSmsAsync(to, message));
        }

        [Fact]
        public void SendEmailAsyncShouldThrowExceptionForWrongMessage()
        {
            const string message = " ";
            const string to = "+16666666666";

            Assert.Throws<ArgumentException>(() => this.smsSenderService.SendSmsAsync(to, message).Exception.InnerException);
        }

        [Fact]
        public void SendEmailAsyncShouldThrowExceptionForWrongPhone()
        {
            const string message = "sal;sakl;a;klsakl;as;klas;lksa";
            const string to = " ";

            Assert.Throws<ArgumentException>(() => this.smsSenderService.SendSmsAsync(to, message).Exception.InnerException);
        }

        //[Fact]
        //public void SendEmailAsyncShouldThrowException()
        //{
        //    const string message = "sal;sakl;a;klsakl;as;klas;lksa";
        //    const string to = "+16666666666";

        //    var smsSenderServiceWithoutPhone = new SmsSender(GlobalConstantsForTest.SID, GlobalConstantsForTest.AuthToken, " ");

        //    Assert.Throws<TwilioException>(() => smsSenderServiceWithoutPhone.SendSmsAsync(to, message).Exception);
        //}
    }
}
