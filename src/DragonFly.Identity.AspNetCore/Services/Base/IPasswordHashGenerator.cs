using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Identity.MongoDB.Services.Base
{
    public interface IPasswordHashGenerator
    {
        byte[] Generate(string username, byte[] salt, string password);

        byte[] GenerateRandomSalt();
    }
}
