using Twilio;
using Twilio.Rest.Verify.V2.Service;

namespace _2FaSms.Services
{
    public class OtpService
    {
    // private readonly string _accountSid;
    // private readonly string _authToken;
    private readonly string _verifyServiceSid;

    public OtpService(string verifyServiceSid)
    {
        // _accountSid = accountSid;
        // _authToken = authToken;
        _verifyServiceSid = verifyServiceSid;
        // TwilioClient.Init(_accountSid, _authToken);
        //TwilioClient.Init(accountSid, authToken);
    }

    public void SendOTP(string phoneNumber)
    {
        var verification = VerificationResource.Create(
            to: phoneNumber,
            channel: "sms",
            pathServiceSid: _verifyServiceSid
        );
    }

    public bool VerifyOTP(string phoneNumber, string code)
    {
        var verificationCheck = VerificationCheckResource.Create(
            to: phoneNumber,
            code: code,
            pathServiceSid: _verifyServiceSid
        );

        return verificationCheck.Status == "approved";
    }

    }
}