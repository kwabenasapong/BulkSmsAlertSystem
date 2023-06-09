using Twilio.Types;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace BulkSmsAlertSystem.Services
{
    public class SmsService
    {
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _fromNumber;

        public SmsService(string accountSid, string authToken, string fromNumber)
        {
            _accountSid = accountSid;
            _authToken = authToken;
            _fromNumber = fromNumber;
        }

        public async Task SendSmsAsync(string toNumber, string message)
        {
            TwilioClient.Init(_accountSid, _authToken);

            await MessageResource.CreateAsync(
                to: new PhoneNumber(toNumber),
                from: new PhoneNumber(_fromNumber),
                body: message);
        }
    }
}
