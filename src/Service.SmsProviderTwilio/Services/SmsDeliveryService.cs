using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Service.SmsProviderTwilio.Settings;
using Service.SmsSender.Grpc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

using SendSmsRequest = Service.SmsSender.Grpc.Models.Requests.SendSmsRequest;
using SendSmsResponse = Service.SmsSender.Grpc.Models.Responses.SendSmsResponse;

namespace Service.SmsProviderTwilio.Services
{
    public class SmsDeliveryService : ISmsDeliveryService
    {
        private readonly ILogger<SmsDeliveryService> _logger;
        private readonly SettingsModel _settingsModel;

        public SmsDeliveryService(ILogger<SmsDeliveryService> logger, SettingsModel settingsModel)
        {
            _logger = logger;
            _settingsModel = settingsModel;
        }

        public Task<SendSmsResponse> SendSmsAsync(SendSmsRequest request)
        {
            var accountSid = Environment.GetEnvironmentVariable(_settingsModel.TwilioAccountSid);
            var authToken = Environment.GetEnvironmentVariable(_settingsModel.TwilioAuthToken);

            TwilioClient.Init(accountSid, authToken);

            request.Phone = request.Phone
                .Replace(" ", "")
                .Replace("-", "")
                .Replace("(", "")
                .Replace(")", "");

            if (!request.Phone.StartsWith('+'))
            {
                request.Phone = $"+{request.Phone}";
            }

            var response = MessageResource.Create(
                body: request.Body,
                from: new Twilio.Types.PhoneNumber(_settingsModel.SenderCompanyPhone),
                to: new Twilio.Types.PhoneNumber(request.Phone)
            );
            
            if (response.Status != MessageResource.StatusEnum.Sent)
            {
                _logger.LogInformation("Sms sending failed.");
                return Task.FromResult(new SendSmsResponse { Status = false, ErrorMessage = "Sms sending failed" });
            }

            _logger.LogInformation("Sms sent successfully");
            return Task.FromResult(new SendSmsResponse { Status = true });
        }
    }
}
