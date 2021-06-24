using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4Service.IdentityServerConfig.IdentityNeeds
{
    public class Constants
    {
        //appsettings
        public static string connStr { get; set; }
        //Stored Procedure strings
        public const string SP_USERLOGIN = "ApiUserLogin";
        public const string SP_GETSALT = "ApiGetSalt";
        public const string USERNAME = "@p_Username";
        public const string PASSWORD = "@p_Password";
    }
}
