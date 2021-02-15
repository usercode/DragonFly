using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Services
{
    public class RandomKeyService
    {
        public RandomKeyService()
        {
            Generator = RandomNumberGenerator.Create();
        }

        private RandomNumberGenerator Generator { get; }

        public string Generate(int length = 64)
        {
            byte[] keyBuffer = new byte[length];
            Generator.GetBytes(keyBuffer);

            return WebEncoders.Base64UrlEncode(keyBuffer);
        }
    }
}
