using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4Service.IdentityServerConfig.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4Service.IdentityServerConfig.Extension
{
    public class IdentityServer4ProfileService : IProfileService
    {
        protected readonly ILogger logger;
        protected readonly IUserRepository userRepositorys;

        public IdentityServer4ProfileService(IUserRepository userRepository, ILogger<IdentityServer4ProfileService> logger)
        {
            this.userRepositorys = userRepository;
            this.logger = logger;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            context.IssuedClaims = userRepositorys.GetClaims();

            return Task.FromResult(0);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = !String.IsNullOrEmpty(userRepositorys.GetUser().Username);

            return Task.FromResult(0);
        }
    }
}
