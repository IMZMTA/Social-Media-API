using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using SocialMedia.Infrastructure;
using SocialMedia.Application;
using SocialMedia.Domain.Config;
using SocialMedia.Domain.Constants;

namespace SocialMedia.Api.Extensions;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddProjectServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AppSettings>(configuration.GetSection(AppConstants.AppSettings));

        services.AddApplication();
        services.AddInfrastructure(configuration);

        return services;
    }
}
