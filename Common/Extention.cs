using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Extention
    {
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
    }
}
