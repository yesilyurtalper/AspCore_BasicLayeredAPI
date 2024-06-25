
using Microsoft.IdentityModel.Tokens;
using BasicLayeredService.API.Constants;

namespace BasicLayeredService.API.Extensions;

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
                ValidAudiences = new[] { "BasicReactClient" },
                ValidateIssuer = true,
                ValidIssuers = new List<string> { 
                    "http://localhost:8080/realms/local_realm",
                    Environment.GetEnvironmentVariable("OIDC_AUTHORITY")
        }
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(APIConstants.BasicLayeredServiceClient, policy =>
            {
                policy.RequireClaim(APIConstants.BasicLayeredServiceClient, APIConstants.BasicLayeredServiceClient);
            });
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(APIConstants.BasicLayeredServiceAdmin, policy =>
            {
                policy.RequireClaim("RealmRole", APIConstants.BasicLayeredServiceAdmin);
            });
        });

        return services;
    }
}
