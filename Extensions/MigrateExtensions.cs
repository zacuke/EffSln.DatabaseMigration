using EffSln.DatabaseMigration.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EffSln.DatabaseMigration.Extensions
{
    public static class MigrateExtensions
    {
        public static async Task RunMigrateAsync(this IServiceScope scope, DatabaseMigrationOptions options, ILogger logger, string connStr, string connectionStringName, string path)
        {
            if (!string.IsNullOrEmpty(connStr))
            {
                if (options.MigrateDbContext != null)
                {
                    await options.MigrateDbContext(scope.ServiceProvider, connStr, logger, connectionStringName);
                }

                logger.LogInformation("Running SQL scripts on {connectionStringName}", connectionStringName);
                connStr.RunSqlScripts(path);
            }
        }
    }
}
