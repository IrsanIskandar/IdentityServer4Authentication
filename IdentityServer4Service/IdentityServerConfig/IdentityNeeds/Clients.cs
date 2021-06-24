using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer4Service.IdentityServerConfig.IdentityNeeds
{
    public class Clients
    {
        public static IEnumerable<Client> GetClient()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "WebApi_26052021141093",
                    ClientName = "Identity Server 4 Authentication and Authorization Web API",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = { "openid", "profile", "email", "role", "policy", "IdentityServer4.API" },
                    ClientSecrets = new List<Secret> { new Secret("1D3nt1Ty_5eRv3R_W3b_4p1!".Sha256()) },
                    AlwaysSendClientClaims = true,
                    IncludeJwtId = true,
                    RequireConsent = false,
                    AllowAccessTokensViaBrowser = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AccessTokenLifetime = 900,
                    IdentityTokenLifetime = 900,
                    SlidingRefreshTokenLifetime = 300,
                    AbsoluteRefreshTokenLifetime = 300,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    RedirectUris = { "" },
                    PostLogoutRedirectUris = { "" },
                },

                new Client
                {
                    ClientId = "MobileApi_03061993141093",
                    ClientName = "Identity Server 4 Authentication and Authorization Mobile Api",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new List<string> { "openid", "profile", "email", "Roles", "policys", "IdentityServer4.MOBILEAPI" },
                    ClientSecrets = new List<Secret> { new Secret("1D3nt1Ty_5eRv3R_Mob1L3_4p1!".Sha256()) },
                    AlwaysSendClientClaims = true,
                    IncludeJwtId = true,
                    RequireConsent = false,
                    AllowAccessTokensViaBrowser = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AccessTokenLifetime = 900,
                    IdentityTokenLifetime = 900,
                    SlidingRefreshTokenLifetime = 300,
                    AbsoluteRefreshTokenLifetime = 300,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    RedirectUris = { "" },
                    PostLogoutRedirectUris = { "" },
                    //AllowedCorsOrigins = new List<string>
                    //{
                    //    "http://localhost:2542/",
                    //    "https://localhost:5001",
                    //    "http://localhost:5000"
                    //}
                }
            };
        }
    }
}
