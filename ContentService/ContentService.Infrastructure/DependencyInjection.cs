using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ContentService.Application;
using ContentService.Infrastructure.Repositories;

namespace ContentService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IForumRepository, ForumRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork<ContentContext>>();

            // Add-Migration InitialMigration -Context ContentContext -Project ContentService.DatabaseMigration
            // Update-Database -Context ContentContext -Project ContentService.DatabaseMigration
            services.AddDbContext<ContentContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("ContentDbConnection"),
                    x => x.MigrationsAssembly("ContentService.DatabaseMigration")));

            return services;
        }
    }
}
