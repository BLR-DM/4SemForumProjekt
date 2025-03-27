using ContentService.Application.Commands;
using ContentService.Application.Commands.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ContentService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IForumCommand, ForumCommand>();

            return services;
        }
    }
}