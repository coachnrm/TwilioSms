using _2FaSms.Models;
using Microsoft.AspNetCore.Mvc;
using Twilio.Clients;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;
using _2FaSms.Services;

namespace _2FaSms.Controllers
{
    public class SmsController : ControllerBase
    {
        private readonly ITwilioRestClient _client;
        public SmsController(ITwilioRestClient client)
        {
            _client = client;
        }

        [HttpPost("api/send-sms")]
        public IActionResult SendSms(SmsMessage model)
        {
            var message = MessageResource.Create(
                to: new PhoneNumber(model.To),
                from: new PhoneNumber(model.From),
                body: model.Message,
                client: _client); // pass in the custom client
            return Ok("Success" + message.Sid);
        }

        [HttpPost("api/send-otp")]
        public IActionResult SendOtp(SendOtpRequest model)
        {
            var otp = GenerateOtp();
            var message = MessageResource.Create(
                to: new PhoneNumber(model.To),
                from: new PhoneNumber(model.From),
                body: $"Your OTP code is {otp}",
                client: _client); // pass in the custom client
            return Ok("Success" + message.Sid);
        }

        private string GenerateOtp()
        {
            Random random = new Random();
            int OtpNumber = random.Next(100000, 999999);
            return OtpNumber.ToString();
        }
    


    }
}