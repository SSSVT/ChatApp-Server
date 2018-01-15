using System;
using System.Security.Cryptography;
using System.Text;

namespace ESChatServer.Areas.v1.Models.Application.Objects
{
    public static class PasswordFactory
    {
        public static string Hash(string password, string salt)
        {
            StringBuilder builder = new StringBuilder(password.Length + salt.Length);
            builder.Append(password.Substring(0, password.Length / 2));
            builder.Append(salt);
            builder.Append(password.Substring(password.Length / 2 + 1));

            return BitConverter.ToString(new SHA512CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(builder.ToString()))).Replace("-", String.Empty).ToUpper();
        }

        public static string GenerateSalt(int length = 128)
        {
            string chars = @"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!#$%&'()*+,-./0123456789:;<=>?@[]^_{}|";
            StringBuilder sb = new StringBuilder(length);
            Random r = new Random();
            for (int i = 0; i < length; i++)
            {
                sb.Append(chars[r.Next(chars.Length)]);
            }
            return sb.ToString();
        }
    }
}
