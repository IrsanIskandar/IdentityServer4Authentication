using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer4Service.IdentityServerConfig.IdentityNeeds
{
    public class ApiResources
    {
        public static IEnumerable<IdentityResource> GetIdentity()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = "role",
                    Required = true,
                    DisplayName = "Roles",
                    ShowInDiscoveryDocument = true,
                    UserClaims = new List<string> { "SuperAdmin", "Admin", "Staff", "Visitor" }
                },
                new IdentityResource
                {
                    Name = "policy",
                    Required = true,
                    DisplayName = "Policys",
                    ShowInDiscoveryDocument = true,
                    UserClaims = new List<string> { "DbAdmin", "SysAdmin", "MgmtStaff", "MgmtVisitor" }
                }
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource() {
                    Name = "IdentityServer4Auth",
                    DisplayName = "Identity Server 4 Authentication and Authorization",
                    UserClaims = new List<string> { "SuperAdmin", "Admin", "Staff", "Visitor", JwtClaimTypes.Role, System.Security.Claims.ClaimTypes.Role },
                    ApiSecrets = new List<Secret> { new Secret("1D3nt1TyS3rV3RS3cr37W3b4p1".Sha256()) },
                    Scopes = new List<string>
                    {
                        "IdentityServer4.API",
                        "IdentityServer4.MOBILEAPI"
                    }
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
                new ApiScope("IdentityServer4.API"),
                new ApiScope("IdentityServer4.MOBILEAPI")
            };
        }
    }

    public class UsersTemp
    {
        public static List<IdentityServer4.Test.TestUser> Get()
        {
            return new List<IdentityServer4.Test.TestUser>
            {
                new IdentityServer4.Test.TestUser
                {
                    SubjectId = "3D9737B9-68C9-4D21-8E27-3CF5F30C0B0C",
                    Username = "admin",
                    Password = "admin",
                    Claims = new List<System.Security.Claims.Claim>
                    {
                        new System.Security.Claims.Claim(JwtClaimTypes.Email, "scott@scottbrady91.com"),
                        new System.Security.Claims.Claim(JwtClaimTypes.Role, "Super Admin"),
                    }
                }
            };
        }
    }
}
