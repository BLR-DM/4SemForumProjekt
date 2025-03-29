using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SubscriptionService.Application.Commands.Interfaces;

namespace SubscriptionService.Application.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IForumSubCommand, IForumSubCommand>();
            services.AddScoped<IPostSubCommand, IPostSubCommand>();

            return services;
        }
    }
}
