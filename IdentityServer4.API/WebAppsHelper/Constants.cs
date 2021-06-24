using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.API.WebAppsHelper
{
    public class Constants
    {
        //appsetting
        public static string connStr { get; set; }
        public static string IdentityServerBaseUrl { get; set; }
        public static string WebApiAppBaseUrl { get; set; }
        public static string WebApiName { get; set; }
        public static string WebApiClientId { get; set; }
        public static string WebApiClientSecret { get; set; }
        public static string MobileApiName { get; set; }
        public static string MobileApiClientId { get; set; }
        public static string MobileApiClientSecret { get; set; }
        public static string ApiSecret { get; set; }
        public static string WebApiScope { get; set; }
        public static string MobileApiScope { get; set; }

        public static string BASE_URL_API_IDENTITYSERVER4 = "";

        public const int TOKEN_256_BYTES = 32;

        //Time on GMT+0 Timezone
        public static readonly DateTime GMT_TIME =
            TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time"));
    }
}
