using Microsoft.OpenApi.Models;
using SocialMedia.Domain.Constants;

namespace SocialMedia.Api.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition(AppConstants.Bearer, new OpenApiSecurityScheme
            {
                Name = AppConstants.Authorization,
                Type = SecuritySchemeType.Http,
                Scheme = AppConstants.Bearer.ToLowerInvariant(),
                BearerFormat = AppConstants.JWT,
                In = ParameterLocation.Header,
                Description = Messages.Description
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference 
                        { 
                            Type = ReferenceType.SecurityScheme, 
                            Id = AppConstants.Bearer 
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }
}
