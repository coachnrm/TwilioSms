using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace _2FaSms.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SendSmsController : ControllerBase
    {
        private readonly string accountSid = "xxxx";
        private readonly string authToken = "xxxx";


         public SendSmsController()
        {
            TwilioClient.Init(accountSid, authToken);
        }

        [HttpPost("send-sms")]
        public IActionResult SendSms([FromBody] SendSmsRequest request)
        {
            try
            {
                var message = MessageResource.Create(
                    body: request.Body,
                    from: new PhoneNumber("+19189217023"), // Your Twilio number
                    to: new PhoneNumber(request.To) // The recipient's phone number
                );

                return Ok(new { MessageSid = message.Sid });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }

    public class SendSmsRequest
    {
        public string To { get; set; }
        public string Body { get; set; }
    }
}