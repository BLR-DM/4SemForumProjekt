using ContentSafetyService.Application.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace ContentSafetyService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IContentSafetyCommand, ContentSafetyCommand>();
        return services;
    }
}