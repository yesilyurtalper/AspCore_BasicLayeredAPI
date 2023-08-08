
using Microsoft.IdentityModel.Tokens;
using BasicLayeredAPI.API.Constants;

namespace BasicLayeredAPI.API.Extensions;

public static class AuthServiceRegistration
{
    public static IServiceCollection AddAuthServices(this  IServiceCollection services)
    {
        services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
        {
            options.Authority = Environment.GetEnvironmentVariable("OIDC_AUTHORITY");
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidAudiences = new[] { "BasicLayeredAPIAPI", "BasicLayeredAPIWebClient_React" },
                ValidateIssuer = true,
                ValidIssuers = new List<string> { 
                    "http://localhost:8080/auth/realms/local_realm",
                    Environment.GetEnvironmentVariable("OIDC_AUTHORITY")
        }
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(APIConstants.BasicLayeredAPIWebClient, policy =>
            {
                policy.RequireClaim(APIConstants.BasicLayeredAPIWebClient, APIConstants.BasicLayeredAPIWebClient);
            });
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(APIConstants.BasicLayeredAPIAdmin, policy =>
            {
                policy.RequireClaim("RealmRole", APIConstants.BasicLayeredAPIAdmin);
            });
        });

        return services;
    }
}
