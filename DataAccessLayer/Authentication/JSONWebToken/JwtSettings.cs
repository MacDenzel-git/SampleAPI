using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Authentication.JSONWebToken
{
    public class JwtSettings
    {
        //who emits the token
        public string Issuer { get; set; }

        //our secret that will be used to generate the token’s signature
        public string Secret { get; set; }
        //how long this token will be valid
        public int ExpirationInDays { get; set; }
    }
}
