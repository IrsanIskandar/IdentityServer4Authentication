using IdentityServer4Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer4Service.IdentityServerConfig.Repository
{
    public interface IUserRepository
    {
        string GetSalt(string username);
        string EncryptPassword(string password, string salt);
        UserApplication Login(string username, string password);
        UserApplication GetUser();
        List<Claim> GetClaims();
    }
}
