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
            // Prepend the country code +66 to the phone number
            string formattedPhoneNumber = FormatPhoneNumber(request.PhoneNumber);
            
            _otpService.SendOTP(formattedPhoneNumber);
            return Ok(new { message = "OTP sent successfully." });
        }

        [HttpPost("verify")]
        public IActionResult VerifyOTP([FromBody] OTPVerifyRequest request)
        {
            // Prepend the country code +66 to the phone number
            string formattedPhoneNumber = FormatPhoneNumber(request.PhoneNumber);
            
            bool isValid = _otpService.VerifyOTP(formattedPhoneNumber, request.OTP);
            return isValid ? Ok(new { message = "OTP verified successfully." }) : Unauthorized(new { message = "Invalid OTP." });
        }

        private string FormatPhoneNumber(string phoneNumber)
        {
            // Check if the phone number already starts with +66 or if it has the local prefix (0)
            if (!phoneNumber.StartsWith("+66"))
            {
                // If it starts with 0, replace it with +66
                if (phoneNumber.StartsWith("0"))
                {
                    return "+66" + phoneNumber.Substring(1);
                }

                // Otherwise, just prepend +66
                return "+66" + phoneNumber;
            }

            return phoneNumber; // Already in the correct format
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