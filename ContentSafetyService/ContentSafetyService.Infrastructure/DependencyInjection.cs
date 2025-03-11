using ContentSafetyService.Infrastructure.Helpers;
using ContentSafetyService.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ContentSafetyService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IRequestBuilder, RequestBuilder>();
        return services;
    }
}