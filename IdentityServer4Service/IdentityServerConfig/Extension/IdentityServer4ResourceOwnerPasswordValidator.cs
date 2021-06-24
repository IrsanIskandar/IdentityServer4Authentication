using IdentityModel;
using IdentityServer4.Validation;
using IdentityServer4Service.IdentityServerConfig.Repository;
using IdentityServer4Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4Service.IdentityServerConfig.Extension
{
    public class IdentityServer4ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserRepository userRepository;

        public IdentityServer4ResourceOwnerPasswordValidator(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            string salt = userRepository.GetSalt(context.UserName);
            if (!String.IsNullOrEmpty(salt))
            {
                string encryptedPassword = userRepository.EncryptPassword(context.Password, salt);
                UserApplication user = userRepository.Login(context.UserName, encryptedPassword);
                if (!String.IsNullOrEmpty(user.Username))
                {
                    context.Result = new GrantValidationResult(user.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
                }
            }
            return Task.FromResult(0);
        }
    }
}
