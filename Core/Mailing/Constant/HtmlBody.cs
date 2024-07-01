namespace Core.Mailing.Constant
{
    public static class HtmlBody
    {
        public static string OtpVerified(string code)
        {
            return $@"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Şifre Sıfırlama</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
        }}
        .container {{
            max-width: 600px;
            margin: 20px auto;
            background-color: #fff;
            border-radius: 5px;
            padding: 20px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }}
        .logo {{
            text-align: center;
            margin-bottom: 20px;
        }}
        .logo img {{
            max-width: 150px;
        }}
        .content {{
            padding: 20px;
        }}
        .button {{
            display: inline-block;
            background-color: #007bff;
            color: #fff;
            text-decoration: none;
            padding: 10px 20px;
            border-radius: 3px;
        }}
        .footer {{
            margin-top: 20px;
            text-align: center;
            color: #666;
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""logo"">
            <img src=""https://media.licdn.com/dms/image/C4D0BAQEcLR0xEqvNjQ/company-logo_200_200/0/1670430830097/tobeto_logo?e=2147483647&v=beta&t=P8qwxIOHx61o4jgQE6Ckb60tfacJxkD1YjZFa1-B9cQ"" alt=""Logo"">
        </div>
        <div class=""content"">
            <h2>Doğrulama Kodu İsteği</h2>
            <p>Merhaba,</p>
            <p>Doğrulama kodu için aşağıdaki numarayı kullanabilirsiniz:</p>
            <p><a class=""button"">{code}</a></p>
            <p>Eğer doğrulama kodu isteğinde bulunmadıysanız, bu e-postayı dikkate almayabilirsiniz.</p>
        </div>
        <div class=""footer"">
            <p>&copy; 2024 Your Company. All rights reserved.</p>
        </div>
    </div>
</body>
</html>";
        }

        public static string PasswordReset(string code)
        {
            return $@"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Şifre Sıfırlama</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
        }}
        .container {{
            max-width: 600px;
            margin: 20px auto;
            background-color: #fff;
            border-radius: 5px;
            padding: 20px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }}
        .logo {{
            text-align: center;
            margin-bottom: 20px;
        }}
        .logo img {{
            max-width: 150px;
        }}
        .content {{
            padding: 20px;
        }}
        .button {{
            display: inline-block;
            background-color: #007bff;
            color: #fff;
            text-decoration: none;
            padding: 10px 20px;
            border-radius: 3px;
        }}
        .footer {{
            margin-top: 20px;
            text-align: center;
            color: #666;
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""logo"">
            <img src=""https://media.licdn.com/dms/image/C4D0BAQEcLR0xEqvNjQ/company-logo_200_200/0/1670430830097/tobeto_logo?e=2147483647&v=beta&t=P8qwxIOHx61o4jgQE6Ckb60tfacJxkD1YjZFa1-B9cQ"" alt=""Logo"">
        </div>
        <div class=""content"">
            <h2>Şifre Sıfırlama İsteği</h2>
            <p>Merhaba,</p>
            <p>Doğrulama kodu için aşağıdaki numarayı kullanabilirsiniz:</p>
            <p><a class=""button"">{code}</a></p>
            <p>Eğer doğrulama kodu isteğinde bulunmadıysanız, bu e-postayı dikkate almayabilirsiniz.</p>
        </div>
        <div class=""footer"">
            <p>&copy; 2024 Your Company. All rights reserved.</p>
        </div>
    </div>
</body>
</html>";
        }

        public static string NewPassword(string password)
        {
            return $@"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Şifre Sıfırlama</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
        }}
        .container {{
            max-width: 600px;
            margin: 20px auto;
            background-color: #fff;
            border-radius: 5px;
            padding: 20px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }}
        .logo {{
            text-align: center;
            margin-bottom: 20px;
        }}
        .logo img {{
            max-width: 150px;
        }}
        .content {{
            padding: 20px;
        }}
        .button {{
            display: inline-block;
            background-color: #007bff;
            color: #fff;
            text-decoration: none;
            padding: 10px 20px;
            border-radius: 3px;
        }}
        .footer {{
            margin-top: 20px;
            text-align: center;
            color: #666;
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""logo"">
            <img src=""https://media.licdn.com/dms/image/C4D0BAQEcLR0xEqvNjQ/company-logo_200_200/0/1670430830097/tobeto_logo?e=2147483647&v=beta&t=P8qwxIOHx61o4jgQE6Ckb60tfacJxkD1YjZFa1-B9cQ"" alt=""Logo"">
        </div>
        <div class=""content"">
            <h2>Yeni Şifre</h2>
            <p>Merhaba,</p>
            <p>Yeni şifreniz için aşağıdaki gibidir:</p>
            <p><a class=""button"">{password}</a></p>
            <p>Sağlık dolu günlere.</p>
        </div>
        <div class=""footer"">
            <p>&copy; 2024 Your Company. All rights reserved.</p>
        </div>
    </div>
</body>
</html>";
        }

        public static string PasswordResetByAdmin(string password)
        {
            return $@"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Şifre Sıfırlama</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
        }}
        .container {{
            max-width: 600px;
            margin: 20px auto;
            background-color: #fff;
            border-radius: 5px;
            padding: 20px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }}
        .logo {{
            text-align: center;
            margin-bottom: 20px;
        }}
        .logo img {{
            max-width: 150px;
        }}
        .content {{
            padding: 20px;
        }}
        .button {{
            display: inline-block;
            background-color: #007bff;
            color: #fff;
            text-decoration: none;
            padding: 10px 20px;
            border-radius: 3px;
        }}
        .footer {{
            margin-top: 20px;
            text-align: center;
            color: #666;
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""logo"">
            <img src=""https://media.licdn.com/dms/image/C4D0BAQEcLR0xEqvNjQ/company-logo_200_200/0/1670430830097/tobeto_logo?e=2147483647&v=beta&t=P8qwxIOHx61o4jgQE6Ckb60tfacJxkD1YjZFa1-B9cQ"" alt=""Logo"">
        </div>
        <div class=""content"">
            <h2>Yeni Şifreniz</h2>
            <p>Merhaba,</p>
            <p>Yeni şifreniz için aşağıdaki gibidir:</p>
            <p><a class=""button"">{password}</a></p>
            <p>Lütfen sisteme giriş yaptıktan sonra şifrenizi değiştiriniz.Sağlık dolu günlere.</p>
        </div>
        <div class=""footer"">
            <p>&copy; 2024 Your Company. All rights reserved.</p>
        </div>
    </div>
</body>
</html>";
        }
    }
}
