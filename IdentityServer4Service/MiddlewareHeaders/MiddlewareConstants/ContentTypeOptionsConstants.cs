using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4Service.MiddlewareHeaders.MiddlewareConstants
{
    public static class ContentTypeOptionsConstants
    {
        public static readonly string Header = "X-Content-Type-Options";
        public static readonly string NoSniff = "nosniff";
    }
}
