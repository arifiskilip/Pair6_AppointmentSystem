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
        public static string VerificationCode
        {
            get
            {
                return "Doğrulama Kodu";
            }
        }
        public static string VerificationCodeTimeout
        {
            get
            {
                return "Doğrulama kodun süresi dolmuş. Lütfen yeni bir kod talep edip tekrar deneyin.";
            }
        }
        public static string IncorrectVerificationCode
        {
            get
            {
                return "Eksik veya hatalı bir kod denediniz. Lütfen doğrulama kodunu kontrol edip tekrar deneyiniz.";
            }
        }
        public static string SuccessVerificationCode
        {
            get
            {
                return "Başarılı bir şekilde aktivasyon sağlandı.";
            }
        }

        public static string PasswordReset
        {
            get
            {
                return "Şifre sıfırlama";
            }
        }
    }
}
