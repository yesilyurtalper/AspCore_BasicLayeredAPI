
using Microsoft.OpenApi.Models;
using BasicLayeredService.API.Models;

namespace BasicLayeredService.API.Extensions;

public static class SwaggerServiceRegistration
{
    public static IServiceCollection AddSwaggerServices(this  IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c => {
            c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Scheme = "bearer"
            });
            c.OperationFilter<AuthenticationRequirementsOperationFilter>();
        });

        return services;
    }
}
