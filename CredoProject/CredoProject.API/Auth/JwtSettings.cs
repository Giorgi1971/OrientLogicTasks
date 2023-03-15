using System;

namespace CredoProject.API.Auth
{
    public class JwtSettings
    {
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public string SecrectKey { get; set; } = null!;
    }
}

