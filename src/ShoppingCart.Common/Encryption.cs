using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Common
{
    public static class Encryption
    {
        public static string Hash(string value,out string stringSalt)
        {
            byte[] salt = new byte[128 / 8];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            stringSalt = Convert.ToBase64String(salt);
            var key = Convert.ToBase64String(KeyDerivation.Pbkdf2(value, salt, KeyDerivationPrf.HMACSHA256, 10000, 256 / 8));
            return key;
        }

        public static bool Validate(string valueHashed,string valueOrigin,string saltValue)
        {
            byte[] salt = Convert.FromBase64String(saltValue);
            var key = Convert.ToBase64String(KeyDerivation.Pbkdf2(valueOrigin, salt, KeyDerivationPrf.HMACSHA256, 10000, 256 / 8));
            return key == valueHashed;
        }
    }
}
