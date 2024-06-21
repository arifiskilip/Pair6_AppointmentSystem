namespace Application.Helpers
{
    public static class VerificationCodeHelper
    {
        public static string GenerateVerificationCode()
        {
            Random random = new Random();
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
