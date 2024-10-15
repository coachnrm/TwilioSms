namespace _2FaSms.Services
{
    public class OtpService
    {
        public string GenerateOtp()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString(); // 6-digit OTP
        }

    }
}