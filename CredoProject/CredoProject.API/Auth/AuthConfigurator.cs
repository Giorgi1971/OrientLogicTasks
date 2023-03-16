using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using CredoProject.Core.Db;
using CredoProject.Core.Db.Entity;

namespace CredoProject.API.Auth
{
    public static class AuthConfigurator
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            var issuer = builder.Configuration["Jwt:Issuer"]!;
            var audience = builder.Configuration["Jwt:Audience"]!;
            var key = builder.Configuration["Jwt:Key"]!;

            builder.Services.Configure<JwtSettings>(s =>
            {
                s.Issuer = issuer;
                s.Audience = audience;
                s.SecrectKey = key;
            });

            builder.Services.AddTransient<TokenGenerator>();

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
            };

            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => { options.TokenValidationParameters = tokenValidationParameters; });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiManager",
                policy => policy.RequireClaim(ClaimTypes.Role, "api-manager"));

                options.AddPolicy("ApiUser",
                policy => policy.RequireClaim(ClaimTypes.Role, "ApiUser"));

                options.AddPolicy("ApiOperator",
                    policy => policy.RequireClaim(ClaimTypes.Role, "ApiOperator"));

                options.AddPolicy("ApiAdmin",
                    policy => policy.RequireClaim(ClaimTypes.Role, "api-admin"));
            });

            builder.Services
            .AddIdentity<UserEntity, RoleEntity>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 4;
                //o.Tokens.
            })
            .AddEntityFrameworkStores<CredoDbContext>()
            .AddDefaultTokenProviders();
        }
    }
}
