using System;
using System.Security.Cryptography;
using AuthService.Services.Interfaces;
using CommonLib.Extensions;

namespace AuthService.Services.Implementations
{
    public class Encrypter : IEncrypter
    {
        private static readonly int DeriveBytesIterationsCount = 10000;
        private static readonly int SaltSize = 50;

        public string GetSalt()
        {
            var saltBytes = new byte[SaltSize];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);

            return Convert.ToBase64String(saltBytes);
        }

        public string GetHash(string value, string salt)
        {
            if (value.Empty())
            {
                throw new ArgumentException("Can't generate hash from an empty value", nameof(value));
            }
            if (salt.Empty())
            {
                throw new ArgumentException("Can't use empty salt", nameof(salt));
            }

            var hash = new Rfc2898DeriveBytes(value, GetBytes(salt), DeriveBytesIterationsCount);
            return Convert.ToBase64String(hash.GetBytes(SaltSize));
        }

        private static byte[] GetBytes(string value)
        {
            var bytes = new byte[value.Length * sizeof(char)];
            Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}
