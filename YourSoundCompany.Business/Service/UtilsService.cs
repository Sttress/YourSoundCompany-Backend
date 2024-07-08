using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace YourSoundCompany.Business.Service
{
    public class UtilsService : IUtilsService
    {
        public UtilsService() { }

        public string GenerateRandomString()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public int CreateRandomCodeInt()
        {
            var random = new Random();
            var code = random.Next(100000, 999999);

            return code;
        }
    }

}
