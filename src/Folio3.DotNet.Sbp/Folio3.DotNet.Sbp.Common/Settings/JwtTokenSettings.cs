using System;
using System.Collections.Generic;
using System.Text;

namespace Folio3.DotNet.Sbp.Common.Settings
{
    public class JwtTokenSettings
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int AccessTokenExpiration { get; set; }
        public int RefreshTokenExpiration { get; set; }
    }
}
