
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SubscriptionService.Infrastructure.Configuration
{
    public class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            // Add-Migration InitialMigration -Context SubscriptionContext -Project SubscriptionService.DatabaseMigration
            // Update-Database -Context SubscriptionContext -Project SubscriptionService.DatabaseMigration

            services.AddDbContext<SubscriptionContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString
                        ("SubscriptionDbConnection"),
                    x =>
                        x.MigrationsAssembly("SubscriptionService.DatabaseMigration")));

            return services;
        }
    }
}
