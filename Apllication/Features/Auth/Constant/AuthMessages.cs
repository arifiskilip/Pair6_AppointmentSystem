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
        public static string UserEmailNotFound
        {
            get
            {
                return "Kullanıcı adı veye şifre hatalı!";
            }
        }
        public static string PasswordsDontMatch
        {
            get
            {
                return "Şifreler eşleşmiyor";
            }
        }
        public static string UserNotFound
        {
            get
            {
                return "Böyle bir kullanıcı bulunamadı";
            }
        }
        public static string CurrentPasswordWrong
        {
            get
            {
                return "Mevcut şifreniz hatalı";
            }
        }
    }
}
