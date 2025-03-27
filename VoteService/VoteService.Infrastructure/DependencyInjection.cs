using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace VoteService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            // Add-Migration InitialMigration -Context VoteContext -Project VoteService.DatabaseMigration
            // Update-Database -Context VoteContext -Project VoteService.DatabaseMigration
            services.AddDbContext<VoteContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString
                        ("VoteDbConnection"),
                    x =>
                        x.MigrationsAssembly("VoteService.DatabaseMigration")));

            return services;
        }
    }
}
