using IdentityServer4Service.IdentityServerConfig.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4Service.IdentityServerConfig.Extension
{
    public static class IdentityServer4BuilderExtensions
    {
        public static IIdentityServerBuilder AddIdentityServer4User(this IIdentityServerBuilder builder)
        {
            builder.Services.AddSingleton<IUserRepository, UserRepository>();
            builder.AddProfileService<IdentityServer4ProfileService>();
            builder.AddResourceOwnerValidator<IdentityServer4ResourceOwnerPasswordValidator>();

            return builder;
        }
    }
}
