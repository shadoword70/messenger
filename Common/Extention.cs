using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DevOne.Security.Cryptography.BCrypt;

namespace Common
{
    public static class Extention
    {
        public static string Salt = "$2a$11$pJbur0SqSGg0tCcjMeMZ0e";

        [Obsolete("Use HashPassword")]
        public static string GetPasswordHash(this string password)
        {
            using (RNGCryptoServiceProvider saltGenerator = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[24];
                saltGenerator.GetBytes(salt);
                using (Rfc2898DeriveBytes hashGenerator = new Rfc2898DeriveBytes(password, salt))
                {
                    hashGenerator.IterationCount = 10101;
                    var passwordBytes = hashGenerator.GetBytes(24);
                    var hash = Convert.ToBase64String(passwordBytes);
                    return hash;
                }
            }
        }

        public static string HashPassword(this string password)
        {
            return BCryptHelper.HashPassword(password, Salt);
        }

        public static bool CheckPassword(this string password)
        {
            return BCryptHelper.CheckPassword(password, Salt);
        }
    }
}
