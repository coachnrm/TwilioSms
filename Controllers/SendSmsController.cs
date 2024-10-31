using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace _2FaSms.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SendSMSController : ControllerBase
    {
        string accountSid = "xxxxxx";
        string authToken = "xxxxxx";

        [HttpPost("SendText")]
        public ActionResult SendText(string phoneNumber)
        {
            TwilioClient.Init(accountSid, authToken);

            var otp = GenerateOtp();
            var message = MessageResource.Create(
                body: $"Your OTP code is {otp}",
                from: new Twilio.Types.PhoneNumber("+19189217023"),
                to: new Twilio.Types.PhoneNumber("+66" + phoneNumber)
            );

            return StatusCode(200, new { message = message.Sid });
        }

        private string GenerateOtp()
        {
            Random random = new Random();
            int OtpNumber = random.Next(100000, 999999);
            return OtpNumber.ToString();
        }
    }
}