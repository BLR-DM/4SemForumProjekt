using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ContentService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            // Add-Migration InitialMigration -Context ContentContext -Project ContentService.DatabaseMigration
            // Update-Database -Context ContentContext -Project ContentService.DatabaseMigration
            services.AddDbContext<ContentContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString
                        ("ContentDbConnection"),
                    x =>
                        x.MigrationsAssembly("ContentService.DatabaseMigration")));
            

            return services;
        }
    }
}
