
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SubscriptionService.Application.Repositories;
using SubscriptionService.Infrastructure.Repositories;

namespace SubscriptionService.Infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IPostSubRepository, PostSubRepository>();
            services.AddScoped<IForumSubRepository, ForumSubRepository>();

            // Add-Migration InitialMigration -Context SubscriptionContext -Project SubscriptionService.DatabaseMigration
            // Update-Database -Context SubscriptionContext -Project SubscriptionService.DatabaseMigration

            services.AddDbContext<SubscriptionContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString
                        ("SubscriptionDbConnection"),
                    x =>
                        x.MigrationsAssembly("SubscriptionService.DatabaseMigration")));

            return services;
        }
    }
}
