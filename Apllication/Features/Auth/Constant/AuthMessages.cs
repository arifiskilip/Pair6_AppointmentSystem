namespace Application.Features.Auth.Constant
{
    public static class AuthMessages
    {
        public static string DuplicateEmail
        {
            get
            {
                return "Bu email adresi zaten mevcut!";
            }
        }
        public static string CustomerEmailNotFound
        {
            get
            {
                return "Kullanıcı adı veye şifre hatalı!";
            }
        }
    }
}
