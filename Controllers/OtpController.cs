using Microsoft.AspNetCore.Mvc;
using _2FaSms.Services;
using Twilio;

namespace _2FaSms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtpController : ControllerBase
    {
        private readonly OtpService _otpService;
        private readonly string accountSid = "xxxx";
        private readonly string authToken = "xxxx";
        private readonly string verifyServiceSid = "xxxx";

        public OtpController()
        {
            // Initialize the Twilio client once
            Twilio.TwilioClient.Init(accountSid, authToken);
            // Pass the Verify Service SID to the OTP service
            _otpService = new OtpService(verifyServiceSid);
        }

        [HttpPost("send")]
        public IActionResult SendOTP([FromBody] OTPRequest request)
        {
            _otpService.SendOTP(request.PhoneNumber);
            return Ok(new { message = "OTP sent successfully." });
        }

        [HttpPost("verify")]
        public IActionResult VerifyOTP([FromBody] OTPVerifyRequest request)
        {
            bool isValid = _otpService.VerifyOTP(request.PhoneNumber, request.OTP);
            return isValid ? Ok(new { message = "OTP verified successfully." }) : Unauthorized(new { message = "Invalid OTP." });
        }
    }

    // Models for request
    public class OTPRequest
    {
        public string PhoneNumber { get; set; }
    }

    public class OTPVerifyRequest
    {
        public string PhoneNumber { get; set; }
        public string OTP { get; set; }
    }
}